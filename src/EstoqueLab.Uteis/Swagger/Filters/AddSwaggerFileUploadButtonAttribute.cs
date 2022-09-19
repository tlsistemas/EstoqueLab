using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueLab.Uteis.Swagger.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AddSwaggerFileUploadButtonAttribute : Attribute
    {
        public String ParameterName { get; set; }
    }
}
