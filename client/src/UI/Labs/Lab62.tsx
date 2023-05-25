import { useState } from "react";
import cl from "../../App.module.css";
import { ApiHelper } from "src/helpers/ApiHelper";
import { AxiosResponse } from 'axios';
import { ErrorHelper } from '../../helpers/ErrorHelper';

const Lab62: React.FC = () => {
    const [btnDis, setBtnDis] = useState(false);
    const [msg, setMsg] = useState("");

    const [brands, setBrands] = useState("Boeing Airbus");
    const [min, setMin] = useState("4");
    const [max, setMax] = useState("7");

    const doStuff = async () => {
        setMsg("Отправка запроса...");
        ErrorHelper.handleOnRequest(setMsg, setBtnDis);

        let response: any;
        try {
            response = await ApiHelper.Lab6.SelectByLastBoardDigit(min, max);
            console.info(response);
        } catch {
            console.info("Exception occured during the request.");
            setMsg("Сервер не прислал валидного ответа.");
        } finally {
            if (response)
                ErrorHelper.handleResponseCode(response.status, response.data.detail, setMsg);
            if (response.status === 200) {
                setMsg(response.data.map(x => `Бортовой номер: ${x.boardidentifier}`).join("\n\n"));
                if(response.data.length ===0) setMsg("Таковых нет.");
            }
        }

        setBtnDis(false);
    }


    return (
        <span className={cl.labCard}>
            <span className={cl.labHeader}>
                Работа №6.2
            </span>
            <span className={cl.labText}>
            Вывести все самолеты, последняя цифра бортового номера
		    которых попадает в диапазон от '...' до '...'.
            </span>
                <span className={cl.inputLabel}>От</span>
                <input onChange={e => { setMin(e.target.value) }} value={min} className={cl.inputValue}></input>
                <span className={cl.inputLabel}>До</span>
                <input onChange={e => { setMax(e.target.value) }} value={max} className={cl.inputValue}></input>
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

export default Lab62;