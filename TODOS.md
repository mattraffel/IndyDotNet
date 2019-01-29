# These are high level project goals that need to be completed before calling it good
[ ] asyncronous calls.  there is plan but completing syncronous use is priority
[ ] Some functions in Pool/Wallet require it to be opened first.  currently
those functions just return when they are not open: no error, no logging etc....
there should be a different behavior:  throw an exception
[ ] Should types wrap/map to their own exceptions (aka WalletException always thrown when 
IndySDK returns an exception)?
[ ] Async files in each namespace were copied from indy-dot-net.  the comments are not always
correct with the changes made for IndyDotNet
[ ] Async files have references to some IndyDotNet types and other cases uses primitives
such as strings.  This is inconsistent.  
[ ] General exception definitions
[ ] Global handler for AggregateException 
[ ] Design by Contract is not enforced.  Consider something like 
https://github.com/Microsoft/CodeContracts