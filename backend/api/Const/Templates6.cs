namespace api.Const;

public class Templates6
{
	/// <summary>
	/// p1 - brands list<br/>
	/// p2 - minimal seats<br/>
	/// p3 - maximum seats
	/// </summary>
	public const string ByBrandAndCapacity_3p = """
		-- 1.
		-- Напишите запрос, который выводит все 
		-- заказы, сделанные 3 и 4 октября 2003 
		-- с использование оператора IN и оператора BETWEEN.
		-- (адаптировать под свою тематику)

		-- Пусть:
		-- Вывести самолеты брендов Airbus и Sukhoi,
		-- количество мест в которых от 200 до 300.

		SELECT * FROM Airplanes WHERE
		    Airplanes.Manufacturer = ANY(@p1) AND
		    Airplanes.SeatsCount BETWEEN @p2 AND @p3;

		-- Successfully run. Total query runtime: 62 msec.
		-- 25 rows affected.
	""";

	/// <summary>
	/// p1 - min last digit<br/>
	/// p2 - max last digit<br/>
	/// </summary>
	public const string ByLastBoardNumber_2p = """
		-- 2.
		-- Напишите запрос, который выводит всех покупателей,
		-- чьи имена начинаются с буквы, попадающей в диапазон 
		-- от «A» до «G».
		-- (адаптировать под свою тематику)

		-- Пусть:
		-- Вывести все самолеты, последняя цифра бортового номера
		-- которых попадает в диапазон от 4 до 7.

		SELECT * FROM Airplanes WHERE
			right(Airplanes.BoardIdentifier, 1) BETWEEN @p1 AND @p2;
	""";

	/// <summary>
	/// p1 - first char is<br/>
	/// </summary>
	public const string WhereDepartureAndStartsWith_1p = """
		-- 3.
		-- Напишите запрос, который выберет всех покупателей, 
		-- чьи имена начинаются с буквы «C».
		-- (адаптировать под свою тематику)

		-- Пусть:
		-- Вывести города, которые значатся как пункт отправления
		-- в списке маршрутов и название которых начинается с буквы 'H'.

		SELECT Routes.DeparturePoint FROM Routes WHERE
			left(Routes.DeparturePoint, 1) ilike @p1;
	""";

	/// <summary>
	/// p1 - number of sold tickets<br/>
	/// </summary>
	public const string WhereSoldN_1p = """
		-- 4.
		-- Напишите запрос, который выберет все заказы, имеющие 
		-- нулевые значения или пропущенные значения NULL в поле 
		-- amt (сумма заказа ).
		-- (адаптировать под свою тематику)

		-- Пусть:
		-- Вывести все перелеты, у которых число проданных билетов
		-- равно нулю.

		SELECT * FROM Flights WHERE
			Flights.SoldCount = @p1;

		-- Successfully run. Total query runtime: 47 msec.
		-- 215 rows affected.
	""";
}
