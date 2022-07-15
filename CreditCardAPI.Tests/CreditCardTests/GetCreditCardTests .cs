using CreditCardAPI.Controllers;
using CreditCardAPI.Data.Common;
using CreditCardAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CreditCardAPI.Tests.CreditCardTests
{
    public class GetCreditCardTests : TestControllerBase
    {
        [Fact]
        public async Task GivenValidGetCreditCardRequest_WhenGetSelectedItem_ThenResultShouldBeTrue()
        {
            string request = "62d0790dd44b1eec5285506b";
            ActionResult result = await ArrangeAndAct(true, request);
            var expectedResponse = result as OkObjectResult;
            var res = expectedResponse?.Value as OperationResponse;

            //ASSERT
            Assert.True(res?.Success == true);
        }

        [Fact]
        public async Task GivenInvalidGetCreditCardRequest_WhenGetSelectedItem_ThenErrorShouldBeCorrect()
        {
            string request = "62d0790dd44b1eec5285506b";
            ActionResult result = await ArrangeAndAct(false, request);
            var expectedResponse = result as NotFoundObjectResult;
            var res = expectedResponse?.Value as string;

            //ASSERT
            Assert.True(res == "Credit card with id 62d0790dd44b1eec5285506b not found");
        }

        [Fact]
        public async Task GivenvalidCreditCardGeteRequest_WhenGetSelectedItem_ThenResponseItemShouldBeCorrect()
        {
            string request = "62d0790dd44b1eec5285506b";
            var responseModel = new CreditCardModel
            {
                Id = "62d0790dd44b1eec5285506b",
                CardHolderName = "Ram",
                AvailableLimit = 100000,
                CardNumber = "4386282337381682",
                CreatedOn = new DateTime(2022, 07, 15),
                UpdatedOn = new DateTime(2022, 07, 15)
            };

            var result = await ArrangeAndAct(true, request);
            var expectedResponse = result as OkObjectResult;
            var res = expectedResponse?.Value as ItemOperationResponse<CreditCardModel>;

            //ASSERT
            Assert.Equal(res?.Item, responseModel);
        }

        private async Task<ActionResult?> ArrangeAndAct(bool isSuccess, string id, bool isCardExists = true)
        {
            //Arrange
            OperationResponse response = new()
            {
                Success = isSuccess
            };

            ItemOperationResponse<CreditCardModel> getCardResponse;

            if (isCardExists == false)
            {
                getCardResponse = new()
                {
                    Success = false,
                    Item = null
                };
            }
            else
            {
                getCardResponse = new()
                {
                    Success = isSuccess,
                    Item = new CreditCardModel
                    {
                        Id = "62d0790dd44b1eec5285506b",
                        CardHolderName = "Ram",
                        AvailableLimit = 100000,
                        CardNumber = "4386282337381682",
                        CreatedOn = new DateTime(2022, 07, 15),
                        UpdatedOn = new DateTime(2022, 07, 15)
                    }
                };
            }
            _creditCardRepository.Setup(repo => repo.GetCreditCardByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(getCardResponse));
            var controller = new CreditCardController(_creditCardRepository.Object);

            // Act
            return await controller.GetCardAsync(id) as ActionResult;
        }
    }
}