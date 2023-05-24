import { useState } from "react";
import cl from "../../App.module.css";
import { ApiHelper } from "src/helpers/ApiHelper";
import { AxiosResponse } from 'axios';
import { ErrorHelper } from '../../helpers/ErrorHelper';

const Lab41: React.FC = () => {
  return (
    <span className={cl.labCard}>
      <span className={cl.labHeader}>
        Работа №4.1
      </span>
      <span className={cl.labText}>
      Определить наличие свободных мест на рейс № 870 31 декабря 2000 года.<br/> 
      Уже выполнено в п. 2.4.
      </span>
    </span>
  );
}

export default Lab41;