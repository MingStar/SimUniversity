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
    [NUnit.Framework.DescriptionAttribute("Setting up boards")]
    public partial class SettingUpBoardsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "BoardSetup.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Setting up boards", "", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("Set up the beginner board for Catan (as from the game rules)")]
        public virtual void SetUpTheBeginnerBoardForCatanAsFromTheGameRules()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Set up the beginner board for Catan (as from the game rules)", ((string[])(null)));
#line 3
this.ScenarioSetup(scenarioInfo);
#line 4
 testRunner.When("I set up the beginner board for Catan");
#line 5
 testRunner.Then("there should be 19 hexagons on the board");
#line 6
 testRunner.And("there should be 54 vectices on the board");
#line 7
 testRunner.And("there should be 72 edges on the board");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Place Type",
                        "Min# of vertices",
                        "Max# of vertices",
                        "Min# of edges",
                        "Max# of edges",
                        "Min# of hexagons",
                        "Max# of hexagons"});
            table1.AddRow(new string[] {
                        "Edge",
                        "2",
                        "2",
                        "2",
                        "4",
                        "1",
                        "2"});
            table1.AddRow(new string[] {
                        "Vertex",
                        "2",
                        "3",
                        "2",
                        "3",
                        "1",
                        "3"});
            table1.AddRow(new string[] {
                        "Hexagon",
                        "6",
                        "6",
                        "6",
                        "6",
                        "3",
                        "6"});
#line 8
 testRunner.And("the adjacent information should be the following:", ((string)(null)), table1);
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Resource",
                        "Count"});
            table2.AddRow(new string[] {
                        "Grain",
                        "4"});
            table2.AddRow(new string[] {
                        "Wood",
                        "4"});
            table2.AddRow(new string[] {
                        "Brick",
                        "3"});
            table2.AddRow(new string[] {
                        "Ore",
                        "3"});
            table2.AddRow(new string[] {
                        "Sheep",
                        "4"});
            table2.AddRow(new string[] {
                        "None",
                        "1"});
#line 13
 testRunner.And("the resource count should be the following:", ((string)(null)), table2);
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "X",
                        "Y",
                        "Number Marker",
                        "Student",
                        "Adj. # of hexes"});
            table3.AddRow(new string[] {
                        "0",
                        "0",
                        "5",
                        "Ore",
                        "3"});
            table3.AddRow(new string[] {
                        "1",
                        "0",
                        "2",
                        "Grain",
                        "4"});
            table3.AddRow(new string[] {
                        "2",
                        "0",
                        "6",
                        "Wood",
                        "3"});
            table3.AddRow(new string[] {
                        "2",
                        "1",
                        "3",
                        "Ore",
                        "4"});
            table3.AddRow(new string[] {
                        "1",
                        "1",
                        "9",
                        "Sheep",
                        "6"});
            table3.AddRow(new string[] {
                        "0",
                        "1",
                        "10",
                        "Sheep",
                        "6"});
            table3.AddRow(new string[] {
                        "-1",
                        "1",
                        "8",
                        "Brick",
                        "4"});
            table3.AddRow(new string[] {
                        "-2",
                        "2",
                        "0",
                        "None",
                        "3"});
            table3.AddRow(new string[] {
                        "-1",
                        "2",
                        "3",
                        "Wood",
                        "6"});
            table3.AddRow(new string[] {
                        "0",
                        "2",
                        "11",
                        "Grain",
                        "6"});
            table3.AddRow(new string[] {
                        "1",
                        "2",
                        "4",
                        "Wood",
                        "6"});
            table3.AddRow(new string[] {
                        "2",
                        "2",
                        "8",
                        "Grain",
                        "3"});
            table3.AddRow(new string[] {
                        "1",
                        "3",
                        "10",
                        "Sheep",
                        "4"});
            table3.AddRow(new string[] {
                        "0",
                        "3",
                        "5",
                        "Brick",
                        "6"});
            table3.AddRow(new string[] {
                        "-1",
                        "3",
                        "6",
                        "Ore",
                        "6"});
            table3.AddRow(new string[] {
                        "-2",
                        "3",
                        "4",
                        "Brick",
                        "4"});
            table3.AddRow(new string[] {
                        "-2",
                        "4",
                        "11",
                        "Wood",
                        "3"});
            table3.AddRow(new string[] {
                        "-1",
                        "4",
                        "12",
                        "Sheep",
                        "4"});
            table3.AddRow(new string[] {
                        "0",
                        "4",
                        "9",
                        "Grain",
                        "3"});
#line 21
 testRunner.And("the details of hexagons should be the following:", ((string)(null)), table3);
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Set up the basic board for Catan")]
        public virtual void SetUpTheBasicBoardForCatan()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Set up the basic board for Catan", ((string[])(null)));
#line 43
this.ScenarioSetup(scenarioInfo);
#line 44
 testRunner.When("I set up the basic board for Catan");
#line 45
 testRunner.Then("there should be 19 hexagons on the board");
#line 46
 testRunner.And("there should be 54 vectices on the board");
#line 47
 testRunner.And("there should be 72 edges on the board");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Resource",
                        "Count"});
            table4.AddRow(new string[] {
                        "Grain",
                        "4"});
            table4.AddRow(new string[] {
                        "Wood",
                        "4"});
            table4.AddRow(new string[] {
                        "Brick",
                        "3"});
            table4.AddRow(new string[] {
                        "Ore",
                        "3"});
            table4.AddRow(new string[] {
                        "Sheep",
                        "4"});
            table4.AddRow(new string[] {
                        "None",
                        "1"});
#line 48
 testRunner.And("the resource count should be the following:", ((string)(null)), table4);
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Place Type",
                        "Min# of vertices",
                        "Max# of vertices",
                        "Min# of edges",
                        "Max# of edges",
                        "Min# of hexagons",
                        "Max# of hexagons"});
            table5.AddRow(new string[] {
                        "Edge",
                        "2",
                        "2",
                        "2",
                        "4",
                        "1",
                        "2"});
            table5.AddRow(new string[] {
                        "Vertex",
                        "2",
                        "3",
                        "2",
                        "3",
                        "1",
                        "3"});
            table5.AddRow(new string[] {
                        "Hexagon",
                        "6",
                        "6",
                        "6",
                        "6",
                        "3",
                        "6"});
#line 56
 testRunner.And("the adjacent information should be the following:", ((string)(null)), table5);
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
