using CreditCardAPI.Controllers;
using CreditCardAPI.Data.Common;
using CreditCardAPI.Data.DTO.Request;
using CreditCardAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CreditCardAPI.Tests.CreditCardTests
{
    public class AddCreditCardTests : TestControllerBase
    {
        [Fact]
        public async Task GivenRequestHasNoItemToSave_WhenSaveSelectedItem_ThenCallToMongoShouldNotBeMade()
        {
            // ARRANGE AND ACT
            var request = new AddCreditCardRequest { CardHolderName = "Ram", CardNumber = "123", AvailableLimit = 10000 };
            await ArrangeAndAct(false, request, true);

            //ASSERT
            _creditCardRepository.Verify(s => s.AddCreditCardAsync(It.IsAny<CreditCardModel>()), Times.Never);
        }

        [Fact]
        public async Task GivenRequestHasValidItemToSave_WhenSaveSelectedItem_ThenCallToMongoShouldBeMade()
        {
            // ARRANGE AND ACT
            var request = new AddCreditCardRequest { CardHolderName = "Ram", CardNumber = "123", AvailableLimit = 10000 };
            await ArrangeAndAct(true, request, false);

            //ASSERT
            _creditCardRepository.Verify(s => s.AddCreditCardAsync(It.IsAny<CreditCardModel>()), Times.Once);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GivenCreditCardAddRequest_WhenSaveSelectedItem_ThenResultShouldBeCorrect(bool validCreditCard)
        {
            var request = new AddCreditCardRequest { CardHolderName = "Ram", CardNumber = "123", AvailableLimit = 10000 };
            OkObjectResult result = await ArrangeAndAct(validCreditCard, request);
            var expectedResponse = result?.Value as OperationResponse;

            //ASSERT
            Assert.True(expectedResponse?.Success == validCreditCard);
        }

        private async Task<OkObjectResult?> ArrangeAndAct(bool isSuccess, AddCreditCardRequest cardRequest, bool isModelError = false)
        {
            //Arrange
            OperationResponse response = new()
            {
                Success = isSuccess
            };

            _creditCardRepository.Setup(repo => repo.AddCreditCardAsync(It.IsAny<CreditCardModel>())).Returns(Task.FromResult(response));
            var controller = new CreditCardController(_creditCardRepository.Object);
            if (isModelError)
                controller.ModelState.AddModelError("CardNumber", "Card number should contain 16 digits length.");

            // Act
            return await controller.AddCardAsync(cardRequest) as OkObjectResult;
        }
    }
}