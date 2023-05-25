import axios from 'axios';
import ep from "../config/endpoints.json";
import Lab64 from '../UI/Labs/Lab64';

export namespace ApiHelper {
    export class Lab1 {
        public static CreateDb = async () => {
            return await axios.post(ep.lab1.CreateDb.Endpoint);
        }
        public static DropDb = async () => {
            return await axios.post(ep.lab1.DropDb.Endpoint);
        }
        public static CreateTables = async () => {
            return await axios.post(ep.lab1.CreateTables.Endpoint);
        }
        public static CreateData = async () => {
            return await axios.post(ep.lab1.CreateRandomData.Endpoint);
        }
    }
    export class Lab2 {
        public static AvgFlyTime = async (brand: string, from: string, to: string) => {
            return await axios.post(ep.lab21.AvgFlyTime.Endpoint + `?`
                + `brand=${brand}` + `&`
                + `from=${from}` + `&`
                + `to=${to}`);
        }
        public static MostlySameRoute = async () => {
            return await axios.post(ep.lab22.MostlySameRoute.Endpoint);
        }
        public static MostlyEmpty70 = async () => {
            return await axios.post(ep.lab23.MostlyEmpty70.Endpoint);
        }
        public static FreeSeats = async (route: string, date: string) => {
            return await axios.post(ep.lab24.FreeSeats.Endpoint + `?`
                + `route=${route}` + `&`
                + `date=${date}`);
        }
    }

    export class Lab4 {
        public static DistanceOnRouteByPlain = async () => {
            return await axios.post(ep.lab42.DistanceOnRouteByPlain.Endpoint);
        }
        public static TimeTable = async (from: string, to: string) => {
            return await axios.post(ep.lab43.TimeTable.Endpoint + `?`
                + `from=${from}` + `&`
                + `to=${to}`);
        }
        public static ChangeCapacity = async (brand: string, delta: string) => {
            return await axios.post(ep.lab44.ChangeCapacity.Endpoint + `?`
                + `brand=${brand}` + `&`
                + `delta=${delta}`);
        }
        public static FlightsOnRoute = async () => {
            return await axios.post(ep.lab45.FlightsOnRoute.Endpoint);
        }
        public static ChangeSpeed = async (brand: string, deltaPercents: string) => {
            return await axios.post(ep.lab410.ChangeSpeed.Endpoint + `?`
                + `brand=${brand}` + `&`
                + `delta=${deltaPercents}`);
        }
    }

    export class Lab6 {
        public static SelectByBrandAndCapacity = async (brand: string[], min: string, max: string) => {
            return await axios.post(ep.lab61.byBrandAndCapcity.Endpoint + `?`
                + `min=${min}` + `&`
                + `max=${max}` + `&`
                + brand.map(x => `brand=${x}`).join('&'));
        }
        public static SelectByLastBoardDigit = async (min: string, max: string) => {
            return await axios.post(ep.lab62.SelectByLastBoardDigit.Endpoint + `?`
                + `min=${min}` + `&`
                + `max=${max}`);
        }
        public static ByDepartureAndFirstChar = async (first: string) => {
            return await axios.post(ep.lab63.ByFirstCharDeparture.Endpoint + `?`
                + `first=${first}`);
        }
        public static WhereSoldN = async (quantity: string) => {
            return await axios.post(ep.lab64.WhereSoldQuantity.Endpoint + `?`
                + `quantity=${quantity}`);
        }
    }
}