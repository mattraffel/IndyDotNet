﻿# Building IndyDotNET

For the most part, all you need to do is Step 4, below.  

To run, you will need to make sure you have IndySDK available, either the binaries or built on your system.  The following
notes should help with that.

## Windows Builds
### Prerequisites
- Visual Studio, rust, git, etc...are installed


### Step 1 Preparation 
Start with reading the directions for [building indy sdk on windows](https://github.com/hyperledger/indy-sdk/blob/master/docs/build-guides/windows-build.md).

The most important step is downloading the [prebuilt dependencies](https://repo.sovrin.org/windows/libindy/deps/).  Alternatively, you can build them yourself.

Building LibIndy is not required if you are able to download the prebuilt LibIndy libraries.  Suggestion is to start [here](https://github.com/hyperledger/indy-sdk/blob/master/docs/build-guides/windows-build.md#build) and work backward if there are any problems.

#### Step 1 Notes
Downloaded openssl from [here](https://slproweb.com/products/Win32OpenSSL.html).  See notes below about installation configuration.


### Step 2 Environment Configuration

TODO: Review->This environment change is only needed if you are building IndySDK

For this example
- all libraries and prebuilt indy-sdk dependencies was put in `d:\engineering\libs`
- during openssl install, selected the option install openssl in bin sub-directory

Set the following environment variables:
```
SET OPENSSL_DIR=C:\PROGRA~1\OPENSS~1\
SET X86_64_PC_WINDOWS_MSVC_OPENSSL_LIB_DIR=%OPENSSL_DIR%lib
SET OPENSSL_INCLUDE_DIR=%OPENSSL_DIR%include
SET X86_64_PC_WINDOWS_MSVC_OPENSSL_INCLUDE_DIR=%OPENSSL_DIR%include
SET SODIUM_LIB_DIR=d:\engineering\libs
SET SODIUM_STATIC=d:\engineering\libs\lib
SET LIBINDY_DIR=d:\engineering\libs
SET INDY_PREBUILT_DEPS_DIR=d:\engineering\libs

SET INDY_PREBUILT_DEPS_DIR=d:\engineering\libs
SET INDY_CRYPTO_PREBUILT_DEPS_DIR=d:\engineering\libs
SET MILAGRO_DIR=d:\engineering\libs
SET LIBZMQ_PREFIX=d:\engineering\libs
```

#### Step 2 notes
- Use short file names and short directory names for the SSL configuration.  There seemed to be some problems with long names.
- Alternatively:  if you chose to build libindy, set `LIBINDY_DIR` to the build output directory.  
This is typically `{libindy source directory}\target\[debug | release]`

### Step 3 (optional)
build indy-sdk (if you can get the windows build of indy-sdk then you can skip this) using the standard `cargo build` command and, 
if necessary, copy dll and lib to `LIBINDY_DIR`

### Step 4 Building IndyDotNet
build IndyDotNet using the standard `dotnet build` command
