using System;
using TechTalk.SpecFlow;
using Calc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpecFlowTrial
{
    [Binding]
    public class GmailLoginSteps
    {
        private int result;

        private Calculator calculator = new Calculator();
        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int number)
        {
            //Conte
            calculator.FirstNumber = number;
            // ScenarioContext.Current.Pending();
        }
        
        [Given(@"I have also entered (.*) into the calculator")]
        public void GivenIHaveAlsoEnteredIntoTheCalculator(int number)
        {
            calculator.SecondNumber = number;
            //ScenarioContext.Current.Pending();
        }
        
        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            result = calculator.Add();
            //ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int expectedResult)
        {
            Assert.AreEqual(expectedResult, result);
            //ScenarioContext.Current.Pending();
        }
    }
}
