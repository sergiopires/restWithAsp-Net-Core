﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace restWithAspNetCore.Controllers
{
    [Route("api/[controller]")]
    public class CalculatorController : Controller
    {


        // GET api/values/5
        [HttpGet("{firstNumber}/{secondNumber}")]
        public IActionResult Sum(string firstNumber, string secondNumber)
        {
            if (isNumeric(firstNumber) && isNumeric(secondNumber))
            {

                var sum = convertToDecimal(firstNumber) + convertToDecimal(secondNumber);

                return Ok(sum.ToString());
            }


            return BadRequest("Invalid Input");
        }

        private Decimal convertToDecimal(string number)
        {
            decimal decimalValue;
            if (decimal.TryParse(number, out decimalValue))
            {
                return decimalValue;
            }
            return 0;
        }

        private bool isNumeric(string strNumber)
        {
            double number;
            bool isNumber = double.TryParse(
                strNumber,
                System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo, out number
                );

            return isNumber;
        }
    }
}