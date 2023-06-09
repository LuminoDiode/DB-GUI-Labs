import { useState } from "react";
import cl from "../../App.module.css";
import { ApiHelper } from "src/helpers/ApiHelper";
import { AxiosResponse } from 'axios';
import { ErrorHelper } from '../../helpers/ErrorHelper';

const Lab42: React.FC = () => {
    const [btnDis, setBtnDis] = useState(false);
    const [msg, setMsg] = useState("");

    const [from, setFrom] = useState("Padbergport");
    const [to, setTo] = useState("Hilllshire");

    const doStuff = async () => {
        setMsg("Отправка запроса...");
        ErrorHelper.handleOnRequest(setMsg, setBtnDis);

        let response: any;
        try {
            response = await ApiHelper.Lab4.DistanceOnRouteByPlain();
            console.info(response);
        } catch {
            console.info("Exception occured during the request.");
            setMsg("Сервер не прислал валидного ответа.");
        } finally {
            if (response)
                ErrorHelper.handleResponseCode(response.status, response.data.detail, setMsg);
            if (response.status === 200) {
                setMsg(response.data.map(x =>
                    `Номер самолета: ${x.airplaneid}\n` +
                    `Номер машрута: ${x.routeid}\n`+
                    `Число перелетов: ${x.count}\n` +
                    `Сумма расстояния: ${x.sum}`).join("\n\n"));
            }
        }

        setBtnDis(false);
    }


    return (
        <span className={cl.labCard}>
            <span className={cl.labHeader}>
                Работы №4.2, 4.5
            </span>
            <span className={cl.labText}>
              4.2 Рассчитать дальность НАЛЕТА каждого самолета по каждому маршруту.<br/>
              4.5 Создать сводную таблицу количества ВЫЛЕТОВ самолетов по маршрутам.
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

export default Lab42;

