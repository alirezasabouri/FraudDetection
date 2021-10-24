# Fraud Detection

Set of REST endpoints which let consumers create new `person` entities and inquiry chance of duplicate entities; you can find project requirement in more details at [Requirements](Requirements.pdf). In this document, you will find instructions, decisions, and relevant technical details regarding how the codebase is developed.

<br/>

## Architecture
The main architecture theme for the project is following **Clean Architecture** principles. It means there is a domain at the core which consists business logic of the application and relevant domain models. The main way of dealing with dependencies is based on **Ports & Adapters** model, therefore the domain would also contain all relevant Ports (*a.k.a Contracts*) .

Business requirements are being implemented as a set of **Usecases** that  relies on other application services, and or contracts that are implemented via pluggable adapters.

## Infrastructure
**Database**
<br/>
MySql is database of choice to achieve persisting functionality for this project. Accordingly, we have a `Database Adapter` that provides us implementation logic for all database repositories, powered by Dapper.

**Docker**
<br/>
We're using docker to setup development environment for running and debugging application



<br/>

## Business requirements 

`Create Person Usecase`
>Create a new Person entity and persists it into the application's database

`Check For Duplicate Person Usecase` 
>Receive two Person entities and determine the similarity rank of those two 

`Similarity Rank` is a decimal value, between 0 to 1, representing the chance of two Person representing the same Physical person, in percentage.

Similarity check is working based on comparing different data fields of two entities and applying comparison logic by using a configurable Rule Engine

<br/>

### Rule Engine
To make the application expandible for future development, comparison checks to determine similarity rank of pair of Persons are being done by a minimal Rule Engine implemented within application services.

A set of pre-defined rules have been prepared to cover current requirements of the application, which can be found in [Ruleset](/src/FraudDetection/Services/RuleEngine/Ruleset) directory.

Applicable rules are configurable by calling `PersonComparisonRuleEngineFactory.AddRule` method, default registration could be checked out [here](src/FraudDetection.Api/AppDependenciesRegistrar.cs#L22).



