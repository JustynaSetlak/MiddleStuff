using CustomModelBinderSample.ModelBinders;
using Microsoft.AspNetCore.Mvc;

namespace CustomModelBinderSample.Models
{
    [ModelBinder(BinderType = typeof(PersonModelBinder))]
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AgeGroup { get; set; }
    }
}
