"# Akkatecture.Test.Eventstore" 

This is a test of [Eventstore](https://eventstore.org/) persistence for [Akkatecture](https://github.com/Lutando/Akkatecture)

It works as of commit [cb997cc](https://github.com/Lutando/Akkatecture/commit/cb997ccb2f3649972982bb32cbd8c6b26fb335d3) from the dev branch

These tests are primarily for experimentation and as such, at the very least you may need to change the `DeviceId` at *line 21* in *DomainTests.cs* between runs.

Also, you'll likely want to run eventstore with the in-memory db using the ``--mem-db`` flag
