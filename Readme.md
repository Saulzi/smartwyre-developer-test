# Smartwyre Developer Test For Tom Salisbury

Unfortunately I have been away etc so have done this using VS Code on a small tablet, It runs with the latest .NET SDK. I have not tested this in VS2022 which supports .net 6 but it should build and run

The payment service has been refactored
 - Adherence to SOLID principals 
 Open Closed - Ability to add additional PaymentSchemes without making changes to this class

 Dependancy Inversion - The class which deals with accounts was instanciated by this class, this is now passed in via a interface, 
 This interface could be split up - Interface segregation.

 Single responsibility - The handling of different payment providers has been split out into multiple PaymentProvider classes,
 These each have the single responsibility for validation that a payment is allowed for each type.. in reality the payments may be different for each of these.

 - Testability
 Facilitated with dependancy inversion.

 - Readability

We�d also like you to 
 - Add some unit tests to the Smartwyre.DeveloperTest.Tests project to show how you would test the code that youve produced

 Note one end to end test for one of the different schemes.

 - Run the PaymentService from the Smartwyre.DeveloperTest.Runner console application

Had to fix up

The only specific 'rules' are:

- The solution should build - Fingers Crossed
- The tests should all pass - May need visual studio runner - used the .Net Core Test runner extension in VS Code 

You are free to use any frameworks/NuGet packages that you see fit.
Added FakeItEasy / FluentAssertions to facilitate unit tests..

 You should plan to spend around 1 hour completing the exercise.
 