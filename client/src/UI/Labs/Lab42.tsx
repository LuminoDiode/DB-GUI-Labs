import { useState } from "react";
import cl from "../../App.module.css";
import { ApiHelper } from "src/helpers/ApiHelper";
import { AxiosResponse } from 'axios';
import { ErrorHelper } from '../../helpers/ErrorHelper';

const Lab42: React.FC = () => {
  return (
    <span className={cl.labCard}>
      <span className={cl.labHeader}>
        Работа №4.2
      </span>
      <span className={cl.labText}>
        Рассчитать дальность полета самолета по каждому маршруту.<br />
        Дальность полета уже записана в таблице в качестве поля.<br />
        SELECT Routes.DistanceKm FROM Routes;
      </span>
    </span>
  );
}

export default Lab42;