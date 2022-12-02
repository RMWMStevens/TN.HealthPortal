﻿using Microsoft.AspNetCore.Mvc;
using TN.HealthPortal.Logic.Entities;

namespace TN.HealthPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeterinariansController : Controller
    {
        [HttpGet]
        [Route("{employeeCode}")]
        public IActionResult GetByEmployeeCode(string employeeCode)
        {
            return Ok(new Veterinarian()
            {
                Name = "John Doe",
                EmployeeCode = employeeCode,
                Farms = new List<Farm>()
                {
                    new Farm
                    {
                        Name = "Van Beek",
                        BlnNumber = "106860",
                        PremiseId = "15",
                        Description = "GN farm producing Z-line",
                        Address = new Address
                        {
                            Street = "Janssen straat",
                            StreetNumber = "58",
                            PostalCode = "8546WD",
                            State = "Gelderland",

                        },
                        Country = new Country
                        {
                            Name = "Nederland",
                            Region = new Region
                            {
                                Name = "Europe"
                            }
                        },
                        ProductionTypes = new List<ProductionType>
                        {
                            new ProductionType()
                            {
                                Name = "Genetic Nucleus"
                            }
                        },
                        Capacity = 3500
                    },
                    new Farm
                    {
                        Name = "Ashorst",
                        BlnNumber = "005630",
                        PremiseId = "16",
                        Description = "Ingene farm in Germany",
                        Address = new Address
                        {
                            Street = "Die strasse",
                            StreetNumber = "",
                            PostalCode = "78546",
                            State = "Noordrijn-Westfalen",
                        },
                        Country = new Country
                        {
                            Name = "Deutschland",
                            Region = new Region
                            {
                                Name = "Europe"
                            }
                        },
                        ProductionTypes = new List<ProductionType>
                        {
                            new ProductionType()
                            {
                                Name = "Ingene"
                            }
                        },
                        Capacity = 7500
                    }
                }
            });
        }
    }
}