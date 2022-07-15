using CreditCardAPI.Data.Repositiory.Interfaces;
using Moq;

namespace CreditCardAPI.Tests
{
    public class TestControllerBase
    {
        protected Mock<ICreditCardRepository> _creditCardRepository;
        public TestControllerBase()
        {
            _creditCardRepository = new Mock<ICreditCardRepository>();
        }
    }
}
