namespace MediatR.DDD.Tests._Mocks
{
    internal sealed class MockRule : IRule
    {
        private readonly bool _isValid;

        public MockRule(bool isValid)
        {
            _isValid = isValid;
        }

        public string Message => "Failed mock rule";

        public bool IsValid()
        {
            return _isValid;
        }
    }
}
