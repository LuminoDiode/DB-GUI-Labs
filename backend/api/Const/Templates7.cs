namespace api.Const;

public class Templates7
{
	/// <summary>
	/// p1 - remove ticket from<br/>
	/// p2 - add ticker to
	/// </summary>
	public const string ChangeTicker_2p = """
		-- Лабораторная работа №7
		-- Необходимо создать форму с реализацией механизма транзакции.  
		-- Перевод и списание некоторых значений между субъектами. Пример: 
		-- банковский перевод. Вводите № клиента с которого списываются 
		-- средства, вводите № клиента на счет которого зачисляются средства. 
		-- После нажатия не кнопку происходит списание с клиента №1, если 
		-- произошел сбой или у клиента №1 недостаточно средств, то 
		-- происходит отмена операции. Иначе средства зачисляются 
		-- на счет клиента №2.

		-- Преобразованная версия согласано варианту "Аэропорт": Замена билета. 
		-- Вводятся два номера рейса. На одном нужно уменьшить число проданных
		-- билетов, на другом - увеличить. Если произошла ошибк или на исходном
		-- самолете не продано ни одного билета или на целевом самолете нет 
		-- свободных мест - происходит отмена.

		-- подключим язык pl/pgSQL
		CREATE EXTENSION IF NOT EXISTS plpgsql;

		-- Функция валидирующая параметры конкретного рейса.
		-- Служит для валидации после транзакции.
		CREATE OR REPLACE FUNCTION ValidateSoldCountForFlight
		(FlightId integer) RETURNS BOOLEAN LANGUAGE plpgsql AS $$ 
		DECLARE
			planeId integer;
			maxCount integer;
			actualCount integer;
		BEGIN
			SELECT AirplaneId, SoldCount INTO planeId, actualCount
				FROM Flights WHERE Id = FlightId;

			SELECT SeatsCount INTO maxCount
				FROM Airplanes WHERE Id = planeId;

			RETURN (
				actualCount >= 0 AND
				actualCount <= maxCount
			);
		END;
		$$;

		-- Процедура смены билета.
		CREATE OR REPLACE PROCEDURE ChangeTicket
		(removeFrom integer, addTo integer)  LANGUAGE plpgsql AS $$ 
		BEGIN
			-- внесение изменений
			UPDATE Flights SET SoldCount = SoldCount - 1
				WHERE Id = removeFrom;

			UPDATE Flights SET SoldCount = SoldCount + 1
				WHERE Id = addTo;

			-- валидация и коммит/аборт
			IF NOT (
				ValidateSoldCountForFlight(removeFrom) AND
				ValidateSoldCountForFlight(addTo)) THEN 
			RAISE EXCEPTION 'Validation failed.';
			END IF;
		END;
		$$;

		-- Транзакция
		BEGIN TRANSACTION;
		CALL ChangeTicket(@p1,@p2);
		COMMIT;

		-- Авто-COMMIT. В случае ошибки - автоматический ROLLBACK.
		-- COMMIT;
	""";
}
