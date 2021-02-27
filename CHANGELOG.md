# F0.Testing
CHANGELOG

## vNext
- Package: Embed icon (fixed _NuGet Warning NU5048_), keep fallback icon URL.

## v0.6.0 (2020-10-22)
- Added assertion for a single custom attribute of a specified type to be applied to a given assembly.
- Added assertion for multiple custom attributes of a specified type to be applied to a given assembly.
- Added assertion for the number of custom attributes of a specified type that are applied to a given assembly.
- Added assertion for the total number of custom attributes of a specified type that are applied to a given assembly.
- Added assertion for the version number of a given assembly.
- Updated `.NET Standard 2.0` dependency on `Microsoft.Bcl.AsyncInterfaces` from `1.1.0` to `1.1.1`.
- Updated `.NET Standard 2.0` dependency on `System.Threading.Tasks.Extensions` from `4.5.3` to `4.5.4`.

## v0.5.0 (2019-12-31)
- Added target framework: `.NET Standard 2.1`.

## v0.4.0 (2019-10-30)
- Added assertion for an exception to surface _immediately_, when retrieving an asynchronous iterator.
- Added assertion for an exception to surface _deferred_, when asynchronously enumerating an async iterator.

## v0.3.0 (2019-10-20)
- Added assertion for an exception to surface _synchronously_, when retrieving an allocation-free asynchronous operation.
- Added assertion for an exception to surface _asynchronously_, when awaiting an allocation-free asynchronous operation.

## v0.2.0 (2019-10-10)
- Added assertion for an exception to surface _synchronously_, when retrieving an asynchronous operation.
- Added assertion for an exception to surface _asynchronously_, when awaiting an asynchronous operation.

## v0.1.0 (2019-08-31)
- Added assertion for an exception to surface _immediately_, when retrieving an iterator.
- Added assertion for an exception to surface _deferred_, when enumerating an iterator.
