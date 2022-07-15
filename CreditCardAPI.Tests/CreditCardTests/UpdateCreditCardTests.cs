using CreditCardAPI.Controllers;
using CreditCardAPI.Data.Common;
using CreditCardAPI.Data.DTO.Request;
using CreditCardAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CreditCardAPI.Tests.CreditCardTests
{
    public class UpdateCreditCardTests : TestControllerBase
    {
        [Fact]
        public async Task GivenRequestHasNoItemToSave_WhenSaveSelectedItem_ThenCallToMongoShouldNotBeMade()
        {
            // ARRANGE AND ACT
            var request = new UpdateCreditCardRequest();
            await ArrangeAndAct(false, request, true);

            //ASSERT
            _creditCardRepository.Verify(s => s.UpdateCreditCardAsync(It.IsAny<CreditCardModel>()), Times.Never);
        }

        [Fact]
        public async Task GivenRequestHasValidItemToSave_WhenSaveSelectedItem_ThenCallToMongoShouldBeMade()
        {
            // ARRANGE AND ACT
            var request = new UpdateCreditCardRequest { Id = "12345", TransationAmount = 1000 };
            await ArrangeAndAct(true, request);

            //ASSERT
            _creditCardRepository.Verify(s => s.UpdateCreditCardAsync(It.IsAny<CreditCardModel>()), Times.Once);
        }


        public async Task GivenCreditCardUpdateRequest_WhenCreditCardNotExists_ThenResultShouldBeCorrect()
        {
            var request = new UpdateCreditCardRequest { Id = "12345", TransationAmount = 1000 };
            ActionResult result = await ArrangeAndAct(true, request);
            var expectedResponse = result as OkObjectResult;
            var res = expectedResponse?.Value as OperationResponse;

            //ASSERT
            Assert.True(res?.Success == true);
        }

        public async Task GivenCreditCardUpdateRequest_WhenCreditCardNotExists_ThenErrorShouldBeCorrect()
        {
            var request = new UpdateCreditCardRequest { Id = "12345", TransationAmount = 1000 };
            ActionResult result = await ArrangeAndAct(false, request);
            var expectedResponse = result as NotFoundObjectResult;
            var res = expectedResponse?.Value as string;

            //ASSERT
            Assert.True(res == "Credit card with id 62d0790dd44b1eec5285506b not found");
        }

        private async Task<ActionResult?> ArrangeAndAct(bool isSuccess, UpdateCreditCardRequest cardRequest, bool isModelError = false)
        {
            //Arrange
            OperationResponse response = new()
            {
                Success = isSuccess
            };

            ItemOperationResponse<CreditCardModel> getCardResponse = new()
            {
                Success = isSuccess,
                Item = new CreditCardModel
                {
                    Id = "62d0790dd44b1eec5285506b",
                    CardHolderName = "Ram",
                    AvailableLimit = 100000,
                    CardNumber = "4386282337381682",
                    CreatedOn = new DateTime(2022, 07, 15)
                }
            };

            _creditCardRepository.Setup(repo => repo.GetCreditCardByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(getCardResponse));
            _creditCardRepository.Setup(repo => repo.UpdateCreditCardAsync(It.IsAny<CreditCardModel>())).Returns(Task.FromResult(response));
            var controller = new CreditCardController(_creditCardRepository.Object);
            if (isModelError)
                controller.ModelState.AddModelError("Id", "Id is required.");

            // Act
            return await controller.UpdateCardAsync(cardRequest) as ActionResult;
        }
    }
}