# Nearby
This application shows the nearby restaurants, mall, grocery stores etc.., which is available within 5 miles. Also, you can see them through built in `Google Maps`.

## Architecture
In this application, we have followed the MVVM Pattern.

## Concepts

- MVVM
- Singleton
- IoC Container

## Dependent Packages

- [Autofac](https://www.nuget.org/packages/Autofac/)
- [Xamarin Essentials](https://www.nuget.org/packages/Xamarin.Essentials/)
- [Xamarin Forms Maps](https://www.nuget.org/packages/Xamarin.Forms.Maps/)

## Feature

* Nearby Stores, Places within 5 miles.
* Built-in Google Maps

## Note

- Replace your API keys for `Google Nearby Places` and for `Maps` in the following files.

* https://github.com/dinesh4official/Nearby/blob/5e2d1afb3441ddb41b9dccee175c1f02957b495c/NearBy/NearBy.Android/Properties/AndroidManifest.xml
* https://github.com/dinesh4official/Nearby/blob/5e2d1afb3441ddb41b9dccee175c1f02957b495c/NearBy/NearByCore/Constants/APIConstants.cs

## References

* https://developers.google.com/maps/documentation/places/web-service/search#PlaceSearchResults
* https://developers.google.com/maps/documentation/places/web-service/details
