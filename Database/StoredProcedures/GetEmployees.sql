DROP FUNCTION IF EXISTS GetEmployees(
	SortField VARCHAR(50),
	SortDirection VARCHAR(5),
	PageSize INTEGER,
	PageNumber INTEGER,
	Filters JSONB,
	FetchWithPagination BOOLEAN
);

CREATE OR REPLACE FUNCTION GetEmployees(
	SortField VARCHAR(50),
	SortDirection VARCHAR(5),
	PageSize INTEGER DEFAULT 10,
	PageNumber INTEGER DEFAULT 1,
	Filters JSONB DEFAULT '{}'::JSONB,
	FetchWithPagination BOOLEAN DEFAULT TRUE
)
RETURNS JSONB
LANGUAGE plpgsql
AS $$
DECLARE
	filter_conditions TEXT := 'employee."IsActive" = true';
	key VARCHAR(20);
	value JSONB;
	value_list TEXT := '';
	total_rows BIGINT;
	result JSONB;
	pagination TEXT := '';
	column_reference TEXT;
	keys TEXT[] := ARRAY[
		'Id', 
		'EmployeeNumber', 
		'Name', 
		'Email', 
		'Phone', 
		'Type', 
		'IsActive'
	];
	columns TEXT[] := ARRAY[
		'employee."Id"', 
		'employee."EmployeeNumber"', 
		'employee."Name"', 
		'employee."Email"', 
		'employee."Phone"', 
		'employeeType."Type"', 
		'employee."IsActive"'
	];
	index INT;
	invalid_key TEXT;
BEGIN
	-- pagination handling
	IF FetchWithPagination THEN
		IF PageSize > 50 THEN
			RAISE EXCEPTION 'PageSize (%) exceeds the maximum allowed limit (50)', PageSize
			USING ERRCODE = '22023';
		END IF;
		pagination := FORMAT($f$ LIMIT %L OFFSET %L $f$, PageSize, (PageNumber - 1) * PageSize);
	ELSE
		PageNumber := 0;
		PageSize := 0;	
	END IF;
	
	-- sort field validation
	IF SortField IS NULL OR SortField NOT IN (SELECT unnest(keys)) THEN
		RAISE EXCEPTION 'Invalid SortField: %', SortField
		USING ERRCODE = '22023';
  END IF;

  -- sort direction validation
	SortDirection := upper(SortDirection);
	IF SortDirection IS NULL OR SortDirection NOT IN ('ASC', 'DESC') THEN
		RAISE EXCEPTION 'SortDirection must be either "ascending" (ASC) or "descending" (DESC). Provided value: %', SortDirection
		USING ERRCODE = '22023';
	END IF;

	-- filter key validation
	FOR invalid_key IN 
		SELECT key FROM jsonb_object_keys(Filters)
		WHERE key NOT IN (SELECT unnest(keys))
	LOOP
		RAISE EXCEPTION 'Invalid filter key : %', invalid_key
		USING ERRCODE = '22023';
	END LOOP;

	-- builds filter condition
	IF Filters IS NOT NULL THEN
		FOR key, value IN SELECT * FROM jsonb_each(Filters)
		LOOP
			IF jsonb_typeof(value) = 'array' AND jsonb_array_length(value) > 0 THEN
				index := array_position(keys, key);
				column_reference := columns[index];
				
				IF column_reference IS NOT NULL THEN
					value_list := (
							SELECT string_agg(quote_literal(x), ', ')
							FROM jsonb_array_elements_text(value) AS t(x)
						);
					filter_conditions := filter_conditions || FORMAT(' AND %s IN (%s)', column_reference, value_list);
				END IF;
			END IF;
		END LOOP;
	END IF; 
  
	-- total row count
	SELECT COUNT(*) INTO total_rows FROM "Employees" employee
	WHERE employee."IsActive" = true;

	EXECUTE FORMAT(
		$f$
		SELECT jsonb_build_object(
			'Total', %s,
			'PageNumber', %s,
			'PageSize', %s,
			'Data', COALESCE(jsonb_agg(row_to_json(subquery)), '[]'::JSONB)
		)
		FROM(
			SELECT
				employee."Id",
				employee."EmployeeNumber",
				employee."Name",
				employee."Email",
				employee."Phone",
				employee."DateOfBirth",
				employeeType."Type",
				employee."IsActive",
				employee."StartDate",
				CASE 
					WHEN employee."EndDate" = '-infinity' THEN NULL
					ELSE employee."EndDate"
				END AS "EndDate",
				employee."CreatedOn",
				employee."CreatedBy",
				employee."ModifiedOn",
				employee."ModifiedBy"
			FROM "Employees" employee
			INNER JOIN "EmployeeTypes" employeeType ON employee."EmployeeTypeId" = employeeType."Id"
			WHERE %s
			ORDER BY %I %s
			%s
		) AS subquery
		$f$,
		total_rows,
		PageNumber,
		PageSize,
		filter_conditions,
		SortField,
		SortDirection,
		pagination
  ) INTO result;

	RETURN result;
END;
$$;
