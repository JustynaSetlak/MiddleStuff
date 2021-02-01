using AttributeRoutingSample.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AttributeRoutingSample.Controllers
{
    [ApiController]
    [Route("person")]
    public class PersonController : ControllerBase
    {
        private static readonly List<Person> PeopleList = new List<Person>
        {
            new Person
            {
                SchoolId = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Black",
                Age = 20,
                IsOfAge = true,
                DateOfPrimarySchoolGraduation = "10-07-2010"
            }
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(PeopleList);
        }

        [HttpGet("info/{schoolId:guid}")]
        public IActionResult Get(Guid schoolId, [FromQuery] FilterData filterData)
        {
            var data = PeopleList.FirstOrDefault(x => x.SchoolId == schoolId && x.Age >= filterData.MinimumAge && x.LastName == filterData.LastName);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person personData)
        {
            PeopleList.Add(personData);

            return NoContent();
        }
    }
}
