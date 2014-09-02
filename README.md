CompanyPortal
=============

Simple App to brush up MVC and ASP.Net skills

Version 0.01
I will break down the current functionality based off the controllers.

PageController

ErrorPage - I implemented a default error page that the user is directed to on an exception. This page gives an
appropriat error message and then gives then appropriate links to follow.

SuccessPage - This page is for when users complete an action(Add a User, Update a Vote etc.) I have yet to add it
to any pages yet.

NewsController

Index - This controller is a home page for the user on login. It includes the current news stories for the company as well
as any votes the user is included in.

VotePage - The page where a user casts a vote.

VoteResults - The page the user is directed to once they cast their vote. It shows the current rankings of the vote options.

AdminController 

Index - Includes links to all of the functions available to an administrator.

EditCompany - Allows the admin to update the company's information.

AddUser - Allows the Admin to complete the first step of user creation.

AddUserGroups - Is the next step of the user creation where the admin assigns the new user to appropriate groups.

AddVote - Allows the Admin to complete the first step of vote creation.

AddVoteOptions - The next step of vote creation where the admin creates the possible choices for the vote.

AddVoteGroups- The final step of vote creation where the admin assigns the vote to the applicable groups.

SelectUserEdit - The first step of user editing where the Admin selects which user to edit.

UserEdit - The final step of user editing where the Admin edits the user inforation.

AddUserGroups - The page where the admin adds new UserGroups for the company.

Up Next....

The next functionality that will be added is to complete the initial admin functions.
This will be followed with an improved News Page and adding some more robust controls(graphing etc.)
