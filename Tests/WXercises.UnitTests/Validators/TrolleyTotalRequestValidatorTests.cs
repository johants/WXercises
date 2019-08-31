using System.Collections.Generic;
using FluentValidation.TestHelper;
using WXercises.Models;
using WXercises.Validations;
using Xunit;

namespace WXercises.UnitTests.Validators
{
    public class TrolleyTotalRequestValidatorTests
    {
        private readonly TrolleyTotalRequestValidator _sut;

        public TrolleyTotalRequestValidatorTests()
        {
            _sut = new TrolleyTotalRequestValidator();
        }

        [Fact]
        public void should_have_error_when_products_is_null()
        {
            var request = new TrolleyTotalRequest
            {
                 Quantities = new List<ProductQuantity>(),
                 Specials =  new List<ProductSpecial>()
            };

            _sut.ShouldHaveValidationErrorFor(r => r.Products, request);
        }

        [Fact]
        public void should_have_error_when_specials_is_null()
        {
            var request = new TrolleyTotalRequest
            {
                Quantities = new List<ProductQuantity>(),
                Products = new List<BaseProduct>()
            };

            _sut.ShouldHaveValidationErrorFor(r => r.Specials, request);
        }

        [Fact]
        public void should_have_error_when_quantities_is_null()
        {
            var request = new TrolleyTotalRequest
            {
                Products = new List<BaseProduct>(),
                Specials = new List<ProductSpecial>()
            };

            _sut.ShouldHaveValidationErrorFor(r => r.Quantities, request);
        }
    }
}
