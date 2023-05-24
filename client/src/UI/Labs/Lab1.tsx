import { useState } from "react";
import cl from "../../App.module.css";
import { ApiHelper } from "src/helpers/ApiHelper";
import { AxiosResponse } from 'axios';
import { ErrorHelper } from '../../helpers/ErrorHelper';

const Lab1: React.FC = () => {
  const [btnDis, setBtnDis] = useState(false);
  const [msg, setMsg] = useState("");

  enum Actions {
    CreateDb,
    CreateTables,
    CreateData,
    DropDb
  }

  const doStuff = async (action: Actions) => {
    ErrorHelper.handleOnRequest(setMsg, setBtnDis);

    let promise: Promise<AxiosResponse<any, any>>;
    switch (action) {
      case Actions.CreateDb:
        promise = ApiHelper.Lab1.CreateDb();
        break;
      case Actions.CreateTables:
        promise = ApiHelper.Lab1.CreateTables();
        break;
      case Actions.CreateData:
        promise = ApiHelper.Lab1.CreateData();
        break;
      case Actions.DropDb:
        promise = ApiHelper.Lab1.DropDb();
        break;
    }

    let response: any;
    try {
      response = await promise;
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
        Работа №1
      </span>
      <span className={cl.labText}>
        Создать структуры таблиц, ключевые и индексные поля. Заполнить таблицы данными, установить связи, удалить данные, восстановить их.
        Предметная область базы данных выбирается в соответствии с вариантом индивидуального задания по номеру
      </span>
      <img className={cl.labImage} src={"https://i.imgur.com/EhiN7uE.png"} />
      <span className={cl.labButtonsList}>
        <button onClick={() => doStuff(Actions.CreateDb)} className={cl.labButton} disabled={btnDis}>Создать базу</button>
        <button onClick={() => doStuff(Actions.CreateTables)} className={cl.labButton} disabled={btnDis}>Создать таблицы</button>
        <button onClick={() => doStuff(Actions.CreateData)} className={cl.labButton} disabled={btnDis}>Залить данные</button>
        <button onClick={() => doStuff(Actions.DropDb)} className={cl.labButton} disabled={btnDis}>Дропнуть базу</button>
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

export default Lab1;

