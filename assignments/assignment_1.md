
# Assignment 1

The purpose of the following exercise is to get you familiar with the more advanced .NET concepts to get you ready to do some serious server programming.

The exercise uses an API for the HSL City Bikes. If you don't care for City Bikes you can feel free to do a variation of this exercise with any other public API. Here are some game related API's (some of which might require registeration):
https://www.programmableweb.com/news/top-10-games-apis-eve-online-riot-games-battle.net/analysis/2015/11/25

---

## 1. Create new application

Create a console application in a empty folder with ``dotnet new console`` -command. This application will print out the number of available city bikes in a requested station.

Start by modifying the Program.cs with the following lines of code:

```
static void Main(string[] args)
{
    Console.WriteLine(args[0]);
}
```

It will make the application to print the commandline argument passed.

Run the app with the following command ``dotnet run station_name`` inside the project folder (``station_name`` is just a random string passed to the application at this point).

---

## 2. Create an interface

Create the following interface:

```
public interface ICityBikeDataFetcher
{
    Task<int> GetBikeCountInStation(string stationName);
}
```

---

## 3. Add a dependency

Next we will add a dependency to a JSON-library which we will use to parse the JSON-payload returned from the API into a C# object graph. Add the following to your "projectname.csproj" -file (between the <Project> -tags):

```
<ItemGroup>
  <PackageReference Include="Newtonsoft.Json" Version="9.0.1" />
</ItemGroup>
```

Use ``dotnet restore`` command to resolve the dependency.

---

## 4. Create a class implementing the interface

Create a class called RealTimeCityBikeDataFetcher which implements the ICityBikeDataFetcher and queries the API ``http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental``

### Hints for implementation:

- You can use System.Net.Http.HttpClient to do the request with a method called ``GetAsync``
- Remember to use ``async`` and ``await`` keywords in the implementation of a asynchronous method
- When you have the HttpResponseMessage from ``GetAsync``, find a way to read it as a byte array and then convert it to string using ``System.Text.Encoding.UTF8.GetString``
- Copy paste the API Url into your browser and check what it returns, find a way to deserialize the string to a C# object using ``JsonConvert.DeserializeObject``
- Find the information you are looking for (bike count in a certain station) and return it as a result from the method
- In the Main method of your application you need to use ``Task.Wait()`` when calling a method with a Task as the return type (there can't be asynchronous code in the main method)

---

## 5. Throw an exception

Throw ArgumentException (provided by .NET framework) if the user calls the "GetBikeCountInStation" with a string which contains numbers. Catch it in the calling code and print "Invalid argument: " and the message property of the exception.

---

## 6. Create and throw your own exception

Create your own Exception called NotFoundException. Throw it if the station can not be found. Catch it in the calling code and print "Not found: " and the message property of the exception.

---

## 7. Create an alternative implementation

Create a class called OfflineCityBikeDataFetcher which also implements the ICityBikeDataFetcher. Get a file called ``bikedata.txt`` from the repository of this course. Find a way to read the requested data from the file.

---

## 8. Implement commandline arguments

Make the console application to accept an additional string argument, ``offline`` or ``realtime``, and decide the implementation of ICityBikeDataFetcher based on that.