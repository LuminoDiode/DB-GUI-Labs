import { useState } from "react";
import cl from "../../App.module.css";
import { ApiHelper } from "src/helpers/ApiHelper";
import { AxiosResponse } from 'axios';
import { ErrorHelper } from '../../helpers/ErrorHelper';

const Lab24: React.FC = () => {
  const [btnDis, setBtnDis] = useState(false);
  const [msg, setMsg] = useState("");

  const [route, setRoute] = useState("870");
  const [date, setDate] = useState("2000-08-03");


  const doStuff = async () => {
    setMsg("Отправка запроса...");
    ErrorHelper.handleOnRequest(setMsg, setBtnDis);

    let response: any;
    try {
      response = await ApiHelper.Lab2.FreeSeats(route,date);
      console.info(response);
    } catch {
      console.info("Exception occured during the request.");
      setMsg("Сервер не прислал валидного ответа.");
    } finally {
      if (response)
        ErrorHelper.handleResponseCode(response.status, response.data.detail, setMsg);
      if (response.status === 200) {
        setMsg(`Запись найдена. `
        + `Всего мест на рейс: ${response.data[0].seatstotal}. `
        + `Из них продано: ${response.data[0].seatssold}. `
        + `Есть свобдные места: ${response.data[0].havefreeseats ? "да" : "нет"}. `);
      }
    }

    setBtnDis(false);
  }


  return (
    <span className={cl.labCard}>
      <span className={cl.labHeader}>
        Работа №2.4
      </span>
      <span className={cl.labText}>
        Определить наличие свободных мест на рейс '...' на дату '...'.
      </span>
      <span className={cl.inputBlock}>
        <span className={cl.inputLabel}>Номер рейса</span>
        <input onChange={e => { setRoute(e.target.value) }} value={route} className={cl.inputValue}></input>
        <span className={cl.inputLabel}>Дата</span>
        <input onChange={e => { setDate(e.target.value) }} value={date} className={cl.inputValue}></input>
      </span>
      <span className={cl.labButtonsList}>
        <button onClick={() => doStuff()} className={cl.labButton} disabled={btnDis}>Запросить</button>
      </span>
      {msg
        ? <span className={cl.serverResponse}>
          {msg}
        </span>
        : <span />
      }
    </span>
  );
}

export default Lab24;

