import { useState } from "react";
import cl from "../../App.module.css";
import { ApiHelper } from "src/helpers/ApiHelper";
import { AxiosResponse } from 'axios';
import { ErrorHelper } from '../../helpers/ErrorHelper';

const Lab23: React.FC = () => {
  const [btnDis, setBtnDis] = useState(false);
  const [msg, setMsg] = useState("");

  const doStuff = async () => {
    setMsg("Отправка запроса...");
    ErrorHelper.handleOnRequest(setMsg, setBtnDis);

    let response: any;
    try {
      response = await ApiHelper.Lab2.MostlyEmpty70();
      console.info(response);
    } catch {
      console.info("Exception occured during the request.");
      setMsg("Сервер не прислал валидного ответа.");
    } finally {
      if (response)
        ErrorHelper.handleResponseCode(response.status, response.data.detail, setMsg);
      if (response.status === 200) {
        setMsg(response.data.map(x => `Номер маршрута: ${x.routeid}.\n`
        + `Число перелетов с заполненностью менее 70%: ${x.less70count}.`).join('\n\n'));
      }
    }

    setBtnDis(false);
  }


  return (
    <span className={cl.labCard}>
      <span className={cl.labHeader}>
        Работа №2.3
      </span>
      <span className={cl.labText}>
        Выбрать маршрут/маршруты, по которым чаще всего летают рейсы, заполненные менее, чем на 70%.
      </span>
      {/* <span className={cl.inputBlock}>
        <span className={cl.inputLabel}>Бренд самолета</span>
        <input onChange={e => { setBrand(e.target.value) }} value={brand} className={cl.inputValue}></input>
        <span className={cl.inputHint} style={{marginTop:"0.1rem"}}>Boeing, Airbus, Sukhoi, etc...</span>
        <span className={cl.inputLabel}>Откуда</span>
        <input onChange={e => { setFrom(e.target.value) }} value={from} className={cl.inputValue}></input>
        <span className={cl.inputLabel}>Куда</span>
        <input onChange={e => { setTo(e.target.value) }} value={to} className={cl.inputValue}></input>
      </span> */}
      <span className={cl.labButtonsList}>
        <button onClick={() => doStuff()} className={cl.labButton} disabled={btnDis}>Запросить</button>
      </span>
      {msg
        ? <span className={cl.serverResponse} style={{whiteSpace:"pre-wrap"}}>
          {msg}
        </span>
        : <span />
      }
    </span>
  );
}

export default Lab23;