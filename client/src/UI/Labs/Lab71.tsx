import { useState } from "react";
import cl from "../../App.module.css";
import { ApiHelper } from "src/helpers/ApiHelper";
import { AxiosResponse } from 'axios';
import { ErrorHelper } from '../../helpers/ErrorHelper';

const Lab71: React.FC = () => {
    const [btnDis, setBtnDis] = useState(false);
    const [msg, setMsg] = useState("");
    
    const [removeFrom, setRemoveFrom] = useState("255");
    const [addTo, setAddTo] = useState("166");

    const doStuff = async () => {
        setMsg("Отправка запроса...");
        ErrorHelper.handleOnRequest(setMsg, setBtnDis);

        let response: any;
        try {
            response = await ApiHelper.Lab7.SwitchTicket(removeFrom,addTo);
            console.info(response);
        } catch {
            console.info("Exception occured during the request.");
            setMsg("Сервер не прислал валидного ответа.");
        } finally {
            if (response)
                ErrorHelper.handleResponseCode(response.status, response.data.detail, setMsg);
        }

        setBtnDis(false);
    }


    return (
        <span className={cl.labCard}>
            <span className={cl.labHeader}>
                Работа №7
            </span>
            <span className={cl.labText}>
                Преобразованная версия согласано варианту "Аэропорт": Замена билета.
                Вводятся два номера рейса. На одном нужно уменьшить число проданных
                билетов, на другом - увеличить. Если произошла ошибка или на исходном
                самолете не продано ни одного билета или на целевом самолете нет
                свободных мест - происходит отмена.
            </span>

            <span className={cl.inputLabel}>Сняться с рейса №</span>
            <input onChange={e => { setRemoveFrom(e.target.value) }} value={removeFrom} className={cl.inputValue}></input>
            <span className={cl.inputLabel}>Записаться на рейс №</span>
            <input onChange={e => { setAddTo(e.target.value) }} value={addTo} className={cl.inputValue}></input>

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

export default Lab71;