# GringottsBank
GringottsBank.WebApi

The lightweight API that handle basic banking operations. Everything is a simple HTTP request and can be used with swagger easily.

Highlights
* Lightweight. Written with .NetCore 5.0.
* Uses JSon as DataModel
* Uses MongoDb from cloud as database, so no need to setup any database environment.
* Failure management.
* Since it is developed by .NetCore its containerizible
* Fully compatible with containerized environments such as Kubernetes.

Usage

You can simply run from Visual Studio or use

	 dotnet run – project <project_path>\GringottsBank.WebApi.csproj

Make sure that you have .Net version 5.0 installed on your computer.

When project starts it will dirtect you to swagger interface and you will be able to control all controller actions.

You need to authorize first before using actions, otherwise you wont be able to run them.

To authorize, you first need to run Pos/Token action from Authentication endpoint. If the credentials are valid, you will claim a token.


when you claim the token clik on Authorize button on the upper right side and enter 
“Bearer <your_token>” and click Authorize.


now you are good to go..




Operations
* Adding / deleting / listing a customer.
* Listing all customers in the bank.
* Adding / Getting accounts of a customer.
* Listing detailed account info of a customer.
* Making a withdraw or add transaction by a customer’s account.
* Listing all transactions of an account.
* Listing transactions by a customer between a time period.

