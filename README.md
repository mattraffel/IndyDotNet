# WHAT

This project provides .NET facade for using IndySDK C callable API. 

# WHY

IndySdk project does include a .NET wrapper.  We believe there is a fundamental 
flaw with its implementation--its not object oriented.  All of the API inputs 
expect developers to know the intrisic details of IndySDK.  Not necessarily a 
bad thing, but it does make it every unfriendly and difficult to use.  

The purpose of this project it to provide a interface to IndySDK that is 
easy to consume and implementation following SOLID engineering principles. 

# LICENSE
MIT
Apache 2.0


# STRUCTURE
IndyDotNet - library 
Test - integration level tests.  See Testing.MD for details on how to set that up
Demo - runnable demos of IndyDotNet (future work)


# See Also
https://github.com/hyperledger/indy-sdk