import axios from 'axios';
import cl from "./App.module.css";
import { BrowserRouter } from 'react-router-dom';
import * as React from 'react';
import Lab1 from './UI/Labs/Lab1';
import Lab21 from './UI/Labs/Lab21';
import Lab22 from './UI/Labs/Lab22';
import Lab23 from './UI/Labs/Lab23';
import Lab24 from './UI/Labs/Lab24';
import Lab41 from './UI/Labs/Lab41';
import Lab42 from './UI/Labs/Lab42';
import Lab43 from './UI/Labs/Lab43';
import Lab44 from './UI/Labs/Lab44';
import Lab45 from './UI/Labs/Lab45';
import Lab410 from './UI/Labs/Lab46(410)';
import Lab64 from './UI/Labs/Lab64';
import Lab61 from './UI/Labs/Lab61';
import Lab62 from './UI/Labs/Lab62';
import Lab63 from './UI/Labs/Lab63';

const App = () => {
  axios.defaults.timeout = /*15*/ 300 * 1000
  axios.defaults.validateStatus = (x) => true;


  return (
    <span className={cl.wrapper}>
      <span className={cl.app}>
        <span className={cl.globalHeader}>Вариант 16</span>
        <Lab1 />

        <Lab21 />
        <Lab22 />
        <Lab23 />
        <Lab24 />

        <Lab41 />
        <Lab42 />
        <Lab43 />
        <Lab44 />
        <Lab45 />
        <Lab410 />

        <Lab61 />
        <Lab62 />
        <Lab63 />
        <Lab64 />
      </span>
    </span>
  );
}

export default App;
