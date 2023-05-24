using System;
using System.Runtime;
using System.Text;
using System.Text.Unicode;
using Bogus;


class Program
{
	public static void Main()
	{
		int AirplaintId, RouteId, FlightId;
		AirplaintId = RouteId = FlightId = 0;


		var airplaneFaker = new Faker<Airplane>()
			.RuleFor(a => a.Id, r => ++AirplaintId)
			.RuleFor(a => a.BoardIdentifier, r => "RA-" + Random.Shared.Next(10 * 1000, 100 * 1000))
			.RuleFor(a => a.Manufacturer, r => r.PickRandom("Airbus", "Boeing", "Sukhoi", "Embraer", "Bombardier"))
			.RuleFor(a => a.CruiseSpeedKmh, r => Random.Shared.Next(450, 901) / 10 * 10)
			.RuleFor(a => a.SeatsCount, r => Random.Shared.Next(20, 400 + 1));
		var airplanes = airplaneFaker.Generate(300).DistinctBy(x => x.BoardIdentifier).ToList();


		var routeFaker = new Faker<Route>()
			.RuleFor(r => r.Id, r => ++RouteId)
			.RuleFor(r => r.DistanceKm, r => Random.Shared.Next(600, 10000 + 1))
			.RuleFor(r => r.DeparturePoint, r => r.Address.City())
			.RuleFor(r => r.DestinationPoint, r => r.Address.City());
		var routes = routeFaker.Generate(1000).DistinctBy(x => System.HashCode.Combine(x.DeparturePoint, x.DestinationPoint)).ToList();

		const int flightsCount = 30 * 1000;

		DateTimeOffset lastFlightDepTime = new();
		int lastRouteId, lastAirplaneId;
		lastRouteId = lastAirplaneId = 0;
		var flightFaker = new Faker<Flight>()
			.RuleFor(f => f.Id, r => ++FlightId)
			.RuleFor(f => f.AirplaneId, r => {
				if(FlightId % (flightsCount/10) == 0) Console.WriteLine($"{ (int)(FlightId / (float)flightsCount * 100) }% {FlightId}");
				while(true) {
					lastAirplaneId = Random.Shared.Next(1, airplanes.Count+1);
					if(airplanes.Any(x => x.Id == lastAirplaneId)) {
						break;
					}
				}
				return lastAirplaneId;
			})
			.RuleFor(f => f.RouteId, r => {
				while(true) {
					lastRouteId = Random.Shared.Next(1, routes.Count+1);
					if(routes.Any(x => x.Id == lastRouteId)) {
						break;
					}
				}
				return lastRouteId;
			})
			.RuleFor(f => f.DepartureTime, r => {
				lastFlightDepTime = r.Date.BetweenOffset(
					new DateTimeOffset(new DateTime(2000, 01, 01), TimeSpan.Zero),
					new DateTimeOffset(new DateTime(2023, 01, 01), TimeSpan.Zero));
				return lastFlightDepTime;
			})
			.RuleFor(f => f.ArrivalTime, r => {
				var last = lastFlightDepTime;
				var dist = routes.Single(x=> x.Id == lastRouteId).DistanceKm;
				var speed = airplanes.Single(x => x.Id == lastAirplaneId).CruiseSpeedKmh;
				var time = TimeSpan.FromHours(dist / (double)speed);

				var result = lastFlightDepTime.Add(time);
				return result;
			})
			.RuleFor(f => f.SoldCount, r => {
				var max = airplanes.Single(x=> x.Id == lastAirplaneId).SeatsCount;
				var min = 0;

				return Random.Shared.Next(min, max + 1);
			});
		Console.WriteLine("Generating flights...");
		var flights = flightFaker.Generate(flightsCount);

		Console.WriteLine("Beginning airplanes.csv...");
		using(var airplanesStream = File.CreateText("airplanes.csv")) {
			for(int i = 0; i < airplanes.Count; i++) {
				var entity = airplanes[i];
				var str =
					$"{entity.Id};" +
					$"{entity.BoardIdentifier};" +
					$"{entity.Manufacturer};" +
					$"{entity.CruiseSpeedKmh};" +
					$"{entity.SeatsCount}";

				airplanesStream.WriteLine(str);
			}
		}

		Console.WriteLine("Beginning routes.csv...");
		using(var routeStream = File.CreateText("routes.csv")) {
			for(int i = 0; i < routes.Count; i++) {
				var entity = routes[i];
				var str =
					$"{entity.Id};" +
					$"{entity.DistanceKm};" +
					$"{entity.DestinationPoint};" +
					$"{entity.DeparturePoint}";

				routeStream.WriteLine(str);
			}
		}

		Console.WriteLine("Beginning flights.csv...");
		using(var flightsStream = File.CreateText("flights.csv")) {
			for(int i = 0; i < flights.Count; i++) {
				var entity = flights[i];
				var str =
					$"{entity.Id};" +
					$"{entity.DepartureTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture)};" +
					$"{entity.ArrivalTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture)};" +
					$"{entity.SoldCount};" +
					$"{entity.AirplaneId};" +
					$"{entity.RouteId}";

				flightsStream.WriteLine(str);
			}
		}
	}
}