# These are high level project goals that need to be completed before calling it good
[ ] Some functions in Pool/Wallet require it to be opened first.  currently
those functions just return when they are not open: no error, no logging etc....
there should be a different behavior:  throw an exception
[ ] Should types wrap/map to their own exceptions (aka WalletException always thrown when 
IndySDK returns an exception)?
