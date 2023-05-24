import endpoints from "../config/endpoints.json"
import { urlJoin } from 'url-join-ts';


export default class UrlHelper {
    public static getHostUrl = () =>
        window.location.protocol + '//' + window.location.host;
}