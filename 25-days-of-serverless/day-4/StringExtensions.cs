using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace day_4
{
    public static class StringExtensions
    {
        public static bool IsValidJson<T>(this string json, out IList<string> errorMessages)
        {
            var jSchemaGenerator = new JSchemaGenerator
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DefaultRequired = Required.AllowNull
            };

            var schema = jSchemaGenerator.Generate(typeof(T));
            var jObject = string.IsNullOrWhiteSpace(json) ? new JObject() : JObject.Parse(json);
            var retval = jObject.IsValid(schema, out IList<string> errors);
            errorMessages = errors;
            return retval;
        }
    }
}
