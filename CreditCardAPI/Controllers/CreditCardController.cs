using CreditCardAPI.Data.DTO.Request;
using CreditCardAPI.Data.Entities;
using CreditCardAPI.Data.Repositiory.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardRepository _creditCardRepository;

        public CreditCardController(ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
        }

        [Route("GetAllCards")]
        [HttpGet]
        public async Task<IActionResult> GetAllCardsAsync()
        {
            var result = await _creditCardRepository.GetAllCardsAsync();
            return Ok(result);
        }

        [HttpGet(Name = "GetCard")]
        public async Task<IActionResult> GetCardAsync(string id)
        {
            var result = await _creditCardRepository.GetCreditCardByIdAsync(id);
            if (result.Success == false || (result.Success == true && result.Item == null))
            {
                return NotFound($"Credit card with id {id} not found");
            }

            return Ok(result);
        }

        [HttpPost(Name = "AddCard")]
        public async Task<IActionResult> AddCardAsync(AddCreditCardRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var model = new CreditCardModel
            {
                CardHolderName = request.CardHolderName,
                CardNumber = request.CardNumber,
                AvailableLimit = request.AvailableLimit,
                CreatedOn = DateTime.UtcNow
            };
            var result = await _creditCardRepository.AddCreditCardAsync(model);
            return Ok(result);
        }

        [HttpPut(Name = "UpdateCard")]
        public async Task<IActionResult> UpdateCardAsync(UpdateCreditCardRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var creditCard = await _creditCardRepository.GetCreditCardByIdAsync(request.Id);
            var item = creditCard.Item;
            if (creditCard.Success == false || (creditCard.Success == true && item == null))
            {
                return NotFound($"Credit card with id {request.Id} not found");
            }

            item.AvailableLimit = item.AvailableLimit - request.TransationAmount;
            item.UpdatedOn = DateTime.UtcNow;

            var result = await _creditCardRepository.UpdateCreditCardAsync(item);
            return Ok(result);
        }

        [HttpDelete(Name = "RemoveCard")]
        public async Task<IActionResult> RemoveCardAsync(string id)
        {
            var creditCard = await _creditCardRepository.GetCreditCardByIdAsync(id);
            if (creditCard.Success == false || (creditCard.Success == true && creditCard.Item == null))
            {
                return NotFound($"Credit card with id {id} not found");
            }

            var result = await _creditCardRepository.RemoveCreditCardAsync(creditCard.Item.Id);
            return Ok(result);
        }
    }
}