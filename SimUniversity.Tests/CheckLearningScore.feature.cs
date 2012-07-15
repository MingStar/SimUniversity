﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.8.1.0
//      SpecFlow Generator Version:1.8.0.0
//      Runtime Version:4.0.30319.235
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace MingStar.SimUniversity.Tests
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.8.1.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Learning Score")]
    public partial class LearningScoreFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "CheckLearningScore.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Learning Score", "", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Calculate tournament score #1")]
        public virtual void CalculateTournamentScore1()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Calculate tournament score #1", ((string[])(null)));
#line 3
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Round Result",
                        "Expected Score"});
            table1.AddRow(new string[] {
                        "10-2",
                        "64"});
            table1.AddRow(new string[] {
                        "10-9",
                        "1"});
            table1.AddRow(new string[] {
                        "3-10",
                        "-49"});
            table1.AddRow(new string[] {
                        "9-10",
                        "-1"});
            table1.AddRow(new string[] {
                        "10-5",
                        "25"});
            table1.AddRow(new string[] {
                        "10-9",
                        "1"});
            table1.AddRow(new string[] {
                        "10-9",
                        "1"});
#line 4
 testRunner.Given("the AI tournament result is as the following:", ((string)(null)), table1);
#line 13
 testRunner.When("the AI tournament result score is calculated");
#line 14
 testRunner.Then("the total round count should be 7");
#line 15
 testRunner.And("the challenger winning count should be 5");
#line 16
 testRunner.And("the tournament score from rounds should be 27");
#line 17
 testRunner.And("the tournament score from winning should be 225");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Calculate tournament score #2")]
        public virtual void CalculateTournamentScore2()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Calculate tournament score #2", ((string[])(null)));
#line 19
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Round Result",
                        "Expected Score"});
            table2.AddRow(new string[] {
                        "10-2",
                        "64"});
            table2.AddRow(new string[] {
                        "10-9",
                        "1"});
            table2.AddRow(new string[] {
                        "3-10",
                        "-49"});
            table2.AddRow(new string[] {
                        "9-10",
                        "-1"});
            table2.AddRow(new string[] {
                        "10-5",
                        "25"});
            table2.AddRow(new string[] {
                        "10-2",
                        "64"});
            table2.AddRow(new string[] {
                        "10-2",
                        "64"});
#line 20
 testRunner.Given("the AI tournament result is as the following:", ((string)(null)), table2);
#line 29
 testRunner.When("the AI tournament result score is calculated");
#line 30
 testRunner.Then("the total round count should be 7");
#line 31
 testRunner.And("the challenger winning count should be 5");
#line 32
 testRunner.And("the tournament score from rounds should be 153");
#line 33
 testRunner.And("the tournament score from winning should be 225");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Calculate tournament score #3")]
        public virtual void CalculateTournamentScore3()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Calculate tournament score #3", ((string[])(null)));
#line 35
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Round Result",
                        "Expected Score"});
            table3.AddRow(new string[] {
                        "2-10",
                        "-64"});
            table3.AddRow(new string[] {
                        "9-10",
                        "-1"});
            table3.AddRow(new string[] {
                        "10-3",
                        "49"});
            table3.AddRow(new string[] {
                        "10-9",
                        "1"});
            table3.AddRow(new string[] {
                        "5-10",
                        "-25"});
            table3.AddRow(new string[] {
                        "2-10",
                        "-64"});
            table3.AddRow(new string[] {
                        "2-10",
                        "-64"});
#line 36
 testRunner.Given("the AI tournament result is as the following:", ((string)(null)), table3);
#line 45
 testRunner.When("the AI tournament result score is calculated");
#line 46
 testRunner.Then("the total round count should be 7");
#line 47
 testRunner.And("the challenger winning count should be 2");
#line 48
 testRunner.And("the tournament score from rounds should be -153");
#line 49
 testRunner.And("the tournament score from winning should be -225");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Calculate tournament score with 7 wins")]
        public virtual void CalculateTournamentScoreWith7Wins()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Calculate tournament score with 7 wins", ((string[])(null)));
#line 51
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Round Result",
                        "Expected Score"});
            table4.AddRow(new string[] {
                        "10-2",
                        "64"});
            table4.AddRow(new string[] {
                        "10-9",
                        "1"});
            table4.AddRow(new string[] {
                        "10-3",
                        "49"});
            table4.AddRow(new string[] {
                        "10-9",
                        "1"});
            table4.AddRow(new string[] {
                        "10-5",
                        "25"});
            table4.AddRow(new string[] {
                        "10-9",
                        "1"});
            table4.AddRow(new string[] {
                        "10-9",
                        "1"});
#line 52
 testRunner.Given("the AI tournament result is as the following:", ((string)(null)), table4);
#line 61
 testRunner.When("the AI tournament result score is calculated");
#line 62
 testRunner.Then("the total round count should be 7");
#line 63
 testRunner.And("the challenger winning count should be 7");
#line 64
 testRunner.And("the tournament score from rounds should be 77");
#line 65
 testRunner.And("the tournament score from winning should be 625");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Calculate tournament score with 6 wins")]
        public virtual void CalculateTournamentScoreWith6Wins()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Calculate tournament score with 6 wins", ((string[])(null)));
#line 67
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Round Result",
                        "Expected Score"});
            table5.AddRow(new string[] {
                        "10-2",
                        "64"});
            table5.AddRow(new string[] {
                        "9-10",
                        "-1"});
            table5.AddRow(new string[] {
                        "10-3",
                        "49"});
            table5.AddRow(new string[] {
                        "10-9",
                        "1"});
            table5.AddRow(new string[] {
                        "10-5",
                        "25"});
            table5.AddRow(new string[] {
                        "10-9",
                        "1"});
            table5.AddRow(new string[] {
                        "10-9",
                        "1"});
#line 68
 testRunner.Given("the AI tournament result is as the following:", ((string)(null)), table5);
#line 77
 testRunner.When("the AI tournament result score is calculated");
#line 78
 testRunner.Then("the total round count should be 7");
#line 79
 testRunner.And("the challenger winning count should be 6");
#line 80
 testRunner.And("the tournament score from rounds should be 77");
#line 81
 testRunner.And("the tournament score from winning should be 625");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion