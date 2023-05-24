import { useState } from "react";
import cl from "../../App.module.css";
import { ApiHelper } from "src/helpers/ApiHelper";
import { AxiosResponse } from 'axios';
import { ErrorHelper } from '../../helpers/ErrorHelper';

const Lab44: React.FC = () => {
    const [btnDis, setBtnDis] = useState(false);
    const [msg, setMsg] = useState("");

    const [brand, setBrand] = useState("Boeing");
    const [delta, setDelta] = useState("5");

    const doStuff = async () => {
        setMsg("Отправка запроса...");
        ErrorHelper.handleOnRequest(setMsg, setBtnDis);

        let response: any;
        try {
            response = await ApiHelper.Lab4.ChangeCapacity(brand, delta);
            console.info(response);
        } catch {
            console.info("Exception occured during the request.");
            setMsg("Сервер не прислал валидного ответа.");
        } finally {
            if (response)
                ErrorHelper.handleResponseCode(response.status, response.data.detail, setMsg);
            if (response.status === 200) {
                setMsg(`Выполнено. Обновлено ${response.data} строк.`);
            }
        }

        setBtnDis(false);
    }


    return (
        <span className={cl.labCard}>
            <span className={cl.labHeader}>
                Работа №4.4
            </span>
            <span className={cl.labText}>
                Увеличить число мест самолетов '...' на '...' человек.
            </span>
            <span className={cl.inputBlock}>
                <span className={cl.inputLabel}>Бренд</span>
                <input onChange={e => { setBrand(e.target.value) }} value={brand} className={cl.inputValue}></input>
                <span className={cl.inputHint} style={{marginTop:"0.1rem"}}>Boeing, Airbus, Sukhoi, etc...</span>
                <span className={cl.inputLabel}>Дельта</span>
                <input onChange={e => { setDelta(e.target.value) }} value={delta} className={cl.inputValue}></input>
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

export default Lab44;

