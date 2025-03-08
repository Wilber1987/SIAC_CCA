ALTER TABLE update_data.parientes_data_update ADD
    migrado bit null;


ALTER TABLE update_data.UpdatedData ADD
    Data_Before_Update_Padres nvarchar(max) null;


ALTER TABLE update_data.UpdatedData ADD
    Data_Before_Update_Alumnos nvarchar(max) null;
