using Domain.Model_Binders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
 [ModelBinder(BinderType = typeof(MetadataValueModelBinder))]
    public class Ingredient
    {
        public string name { get; set; }
        public int weight { get; set; }
    }
}
