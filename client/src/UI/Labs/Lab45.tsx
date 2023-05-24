import { useState } from "react";
import cl from "../../App.module.css";
import { ApiHelper } from "src/helpers/ApiHelper";
import { AxiosResponse } from 'axios';
import { ErrorHelper } from '../../helpers/ErrorHelper';

const Lab45: React.FC = () => {
    const [btnDis, setBtnDis] = useState(false);
    const [msg, setMsg] = useState("");

    const doStuff = async () => {
        setMsg("Отправка запроса...");
        ErrorHelper.handleOnRequest(setMsg, setBtnDis);

        let response: any;
        try {
            response = await ApiHelper.Lab4.FlightsOnRoute();
            console.info(response);
        } catch {
            console.info("Exception occured during the request.");
            setMsg("Сервер не прислал валидного ответа.");
        } finally {
            if (response)
                ErrorHelper.handleResponseCode(response.status, response.data.detail, setMsg);
            if (response.status === 200) {
                setMsg(response.data.map(x=> `Номер маршрута: ${x.routeid}.\n`
                + `Откуда: ${x.departurepoint}. Куда: ${x.destinationpoint}.\n`
                + `ЧИСЛО ВЫЛЕТОВ: ${x.flightscound}.`).join('\n\n'));
            }
        }

        setBtnDis(false);
    }


    return (
        <span className={cl.labCard}>
            <span className={cl.labHeader}>
                Работа №4.5
            </span>
            <span className={cl.labText}>
            Создать сводную таблицу количества вылетов самолетов по маршрутам.
            </span>

            <span className={cl.labButtonsList}>
                <button onClick={() => doStuff()} className={cl.labButton} disabled={btnDis}>Запросить</button>
            </span>
            {msg
                ? <span className={cl.serverResponse} style={{ whiteSpace: "pre-wrap" }}>
                    {msg}
                </span>
                : <span />
            }
        </span>
    );
}

export default Lab45;

