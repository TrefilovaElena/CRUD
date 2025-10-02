DO $$
DECLARE 
    table_name TEXT;
BEGIN
    FOR table_name IN 
        SELECT tablename 
        FROM pg_tables 
        WHERE schemaname = 'public' 
          AND lower(tablename) NOT IN (
              'cities', 
              'applicationcategories', 
              'computercategories', 
              'placecategories', 
              'platforms', 
              'clubs', 
              'floors', 
              'rooms', 
              'bookingtariffs', 
              'achievements', 
              'feedbackresponsibleemails', 
              'secret'
          )
    LOOP
        EXECUTE 'TRUNCATE TABLE public.' || quote_ident(table_name) || ' CASCADE;';
    END LOOP;
END $$;
--------------------------------------------------------------------------------------------------------------------------------------------
DELETE FROM public."Achievements"	WHERE "Id" < 0;
DELETE FROM public."ComputerCategories"	WHERE "Id" < 0;
DELETE FROM public."ApplicationCategories"	WHERE "Id" < 0;
DELETE FROM public."PlaceCategories"	WHERE "Id" < 0;
DELETE FROM public."Platforms"	WHERE "Id" < 0;
DELETE FROM public."Rooms"	WHERE "Id" < 0;
DELETE FROM public."Floors"	WHERE "Id" < 0;
DELETE FROM public."BookingTariffs"	WHERE "Id" < 0;
DELETE FROM public."FeedbackResponsibleEmails"	WHERE "Id" < 0;
--------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO public."FeedbackResponsibleEmails"(	"Id", "Email") VALUES (1, 'mayakarenahelp@yandex.ru');