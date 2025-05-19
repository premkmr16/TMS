DROP FUNCTION IF EXISTS GetTableSchema();

CREATE OR REPLACE FUNCTION GetTableSchema()
    RETURNS JSON
    LANGUAGE plpgsql
AS
$$
DECLARE
    ColumnDetailsJson       JSON;
    ForeignKeyRelationsJson JSON;
BEGIN
    -- query to fetch column names and constraints
    SELECT json_agg(row_to_json(t))
    INTO ColumnDetailsJson
    FROM (SELECT cols.table_name    AS "TableName",
                 cols.column_name   AS "ColumnName",
                 cols.data_type     AS "DataType",
                 cols.is_nullable   AS "IsNullable",
                 tc.constraint_type AS "ConstraintType"
          FROM information_schema.columns cols
                   LEFT JOIN
               information_schema.key_column_usage kcu
               ON cols.table_name = kcu.table_name
                   AND cols.column_name = kcu.column_name
                   LEFT JOIN
               information_schema.table_constraints tc
               ON kcu.constraint_name = tc.constraint_name
                   AND kcu.table_name = tc.table_name
          WHERE cols.table_schema = 'public'
          ORDER BY cols.table_name, cols.ordinal_position) t;

    -- query to fetch the foreign key relation ship between tables
    SELECT json_agg(row_to_json(fk))
    INTO ForeignKeyRelationsJson
    FROM (SELECT tc.table_name   AS "TableName",
                 kcu.column_name AS "ColumnName",
                 ccu.table_name  AS "ReferencedTable",
                 ccu.column_name AS "ReferencedColumn"
          FROM information_schema.table_constraints tc
                   JOIN
               information_schema.key_column_usage kcu
               ON tc.constraint_name = kcu.constraint_name
                   JOIN
               information_schema.constraint_column_usage ccu
               ON ccu.constraint_name = tc.constraint_name
          WHERE tc.constraint_type = 'FOREIGN KEY'
            AND tc.table_schema = 'public'
          ORDER BY tc.table_name) fk;

    -- Return as combined JSON
    RETURN json_build_object(
            'Columns', COALESCE(ColumnDetailsJson, '[]'::JSON),
            'ForeignKeys', COALESCE(ForeignKeyRelationsJson, '[]'::JSON)
           );
END;
$$ 
