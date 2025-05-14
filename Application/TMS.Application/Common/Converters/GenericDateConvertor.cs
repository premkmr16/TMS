using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TMS.Application.Common.Converters;

public class GenericDateConvertor<T> : JsonConverter<T>
{
    private const string DateFormat = "dd-MM-yyyy";

    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();

        if (string.IsNullOrWhiteSpace(str))
        {
            if (typeof(T) == typeof(DateTime)) return (T)(object)DateTime.MinValue;
          
            if (typeof(T) == typeof(DateTimeOffset)) return (T)(object)DateTimeOffset.MinValue;
        }
        else
        {
            if (typeof(T) == typeof(DateTime))  return (T)(object)DateTime.ParseExact(str, DateFormat, CultureInfo.InvariantCulture);
           
            if (typeof(T) == typeof(DateTimeOffset)) return (T)(object)DateTimeOffset.ParseExact(str!, DateFormat, CultureInfo.CurrentCulture);
        }
        
        throw new NotSupportedException();
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (value is DateTime dateTime) writer.WriteStringValue(dateTime == DateTime.MinValue ? "" : dateTime.ToString(DateFormat));
        
        else if (value is DateTimeOffset dateTimeOffset) writer.WriteStringValue(dateTimeOffset == DateTimeOffset.MinValue ? "" : dateTimeOffset.ToString(DateFormat));
        
        else throw new NotSupportedException();
    }
}