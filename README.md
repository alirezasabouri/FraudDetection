# Fraud Detection

Set of REST endpoints which let consumers  to create new `person` entities and inquiry chance of duplicate entities; you can find project requirement in more details at [Requirements](Requirements.pdf),. In this document you will find instructions, decisions, and relevant technical details regarding how the codebase is developed.


## Architecture
The main architecture theme for the project is following **Clean Architecture** principles. It means there is the domain at core which consists business logic of application and relevant domain models. The main model of dealing with dependencies is based on **Ports & Adapters** model, therefore the domain would also contains all relevant Ports (*a.k.a Contracts*) .

## Infrastructure
