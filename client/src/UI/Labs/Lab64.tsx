import { useState } from "react";
import cl from "../../App.module.css";
import { ApiHelper } from "src/helpers/ApiHelper";
import { AxiosResponse } from 'axios';
import { ErrorHelper } from '../../helpers/ErrorHelper';

const Lab64: React.FC = () => {
    const [btnDis, setBtnDis] = useState(false);
    const [msg, setMsg] = useState("");

    const [delta, setDelta] = useState("5");

    const doStuff = async () => {
        setMsg("Отправка запроса...");
        ErrorHelper.handleOnRequest(setMsg, setBtnDis);

        let response: any;
        try {
            response = await ApiHelper.Lab6.WhereSoldN(delta);
            console.info(response);
        } catch {
            console.info("Exception occured during the request.");
            setMsg("Сервер не прислал валидного ответа.");
        } finally {
            if (response)
                ErrorHelper.handleResponseCode(response.status, response.data.detail, setMsg);
            if (response.status === 200) {
                setMsg(response.data.map(x => `Номер рейса: ${x.routeid}\n` +
                    `Отправление: ${x.departuretime.split('T').join(' ')}\n` +
                    `Прибытие:       ${x.arrivaltime.split('T').join(' ')}`).join("\n\n"));
            }
        }

        setBtnDis(false);
    }


    return (
        <span className={cl.labCard}>
            <span className={cl.labHeader}>
                Работа №6.4
            </span>
            <span className={cl.labText}>
                Вывести все перелеты, у которых число проданных билетов равно '...'.
            </span>
            <span className={cl.inputLabel}>Бренд</span>
                <span className={cl.inputLabel}>Число проданных билетов</span>
                <input onChange={e => { setDelta(e.target.value) }} value={delta} className={cl.inputValue}></input>
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

export default Lab64;