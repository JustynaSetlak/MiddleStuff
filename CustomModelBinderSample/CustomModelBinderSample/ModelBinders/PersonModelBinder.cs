using System;
using System.Threading.Tasks;
using CustomModelBinderSample.Constants;
using CustomModelBinderSample.Models;
using CustomModelBinderSample.Models.InputParams;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CustomModelBinderSample.ModelBinders
{
    public class PersonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var ageData = GetBindingValue(bindingContext, nameof(GetPersonInputParams.Age));
            
            var result = new Person
            {
                FirstName = GetBindingValue(bindingContext, nameof(GetPersonInputParams.FirstName)),
                LastName = GetBindingValue(bindingContext, nameof(GetPersonInputParams.LastName)),
                AgeGroup = CalculateGroupAge(bindingContext, ageData)
            };

            bindingContext.Result = ModelBindingResult.Success(result);
            
            return Task.CompletedTask;
        }

        private string GetBindingValue(ModelBindingContext bindingContext, string key)
        {
            return bindingContext.ValueProvider.GetValue(key).FirstValue;
        }

        private string CalculateGroupAge(ModelBindingContext bindingContext, string ageValue)
        {
            var isParsingSuccessful = int.TryParse(ageValue, out var age);

            if (!isParsingSuccessful)
            {
                return AgeGroup.Unspecified;
            }

            return age < 18 ? AgeGroup.Children : AgeGroup.Adults;
        }
    }
}
