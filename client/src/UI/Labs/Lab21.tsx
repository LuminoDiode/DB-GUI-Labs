import { useState } from "react";
import cl from "../../App.module.css";
import { ApiHelper } from "src/helpers/ApiHelper";
import { AxiosResponse } from 'axios';
import { ErrorHelper } from '../../helpers/ErrorHelper';

const Lab21: React.FC = () => {
  const [btnDis, setBtnDis] = useState(false);
  const [msg, setMsg] = useState("");

  const [from, setFrom] = useState("Padbergport");
  const [to, setTo] = useState("Hilllshire");
  const [brand, setBrand] = useState("Boeing");

  const doStuff = async () => {
    setMsg("Отправка запроса...");
    ErrorHelper.handleOnRequest(setMsg, setBtnDis);

    let response: any;
    try {
      response = await ApiHelper.Lab2.AvgFlyTime(brand, from, to);
      console.info(response);
    } catch {
      console.info("Exception occured during the request.");
      setMsg("Сервер не прислал валидного ответа.");
    } finally {
      if (response)
        ErrorHelper.handleResponseCode(response.status, response.data.detail, setMsg);
      if (response.status === 200) {
        setMsg(`Результат: ${response.data[0]["avg"]}`);
      }
    }

    setBtnDis(false);
  }


  return (
    <span className={cl.labCard}>
      <span className={cl.labHeader}>
        Работа №2.1
      </span>
      <span className={cl.labText}>
        Определить среднее расчетное время полета для самолета '...' по маршруту '...' - '...'.
      </span>
      <span className={cl.inputBlock}>
        <span className={cl.inputLabel}>Бренд самолета</span>
        <input onChange={e => { setBrand(e.target.value) }} value={brand} className={cl.inputValue}></input>
        <span className={cl.inputHint} style={{marginTop:"0.1rem"}}>Boeing, Airbus, Sukhoi, etc...</span>
        <span className={cl.inputLabel}>Откуда</span>
        <input onChange={e => { setFrom(e.target.value) }} value={from} className={cl.inputValue}></input>
        <span className={cl.inputLabel}>Куда</span>
        <input onChange={e => { setTo(e.target.value) }} value={to} className={cl.inputValue}></input>
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

export default Lab21;

