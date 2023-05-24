import { useState } from "react";
import cl from "../../App.module.css";
import { ApiHelper } from "src/helpers/ApiHelper";
import { AxiosResponse } from 'axios';
import { ErrorHelper } from '../../helpers/ErrorHelper';

const Lab43: React.FC = () => {
    const [btnDis, setBtnDis] = useState(false);
    const [msg, setMsg] = useState("");

    const [from, setFrom] = useState("Padbergport");
    const [to, setTo] = useState("Hilllshire");

    const doStuff = async () => {
        setMsg("Отправка запроса...");
        ErrorHelper.handleOnRequest(setMsg, setBtnDis);

        let response: any;
        try {
            response = await ApiHelper.Lab4.TimeTable(from, to);
            console.info(response);
        } catch {
            console.info("Exception occured during the request.");
            setMsg("Сервер не прислал валидного ответа.");
        } finally {
            if (response)
                ErrorHelper.handleResponseCode(response.status, response.data.detail, setMsg);
            if (response.status === 200) {
                setMsg(response.data.map(x =>
                    `Отправление: ${x.departuretime.split('T').join(' ')}\n` +
                    `Прибытие:       ${x.arrivaltime.split('T').join(' ')}`).join("\n\n"));
            }
        }

        setBtnDis(false);
    }


    return (
        <span className={cl.labCard}>
            <span className={cl.labHeader}>
                Работа №4.3
            </span>
            <span className={cl.labText}>
                Создать таблицу расписания самолетов по маршруту «...»-«...»
            </span>
            <span className={cl.inputBlock}>
                <span className={cl.inputLabel}>Откуда</span>
                <input onChange={e => { setFrom(e.target.value) }} value={from} className={cl.inputValue}></input>
                <span className={cl.inputLabel}>Куда</span>
                <input onChange={e => { setTo(e.target.value) }} value={to} className={cl.inputValue}></input>
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

export default Lab43;

