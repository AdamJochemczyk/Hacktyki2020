import React , {useState} from "react"
import {useTable, useFilters, useSortBy} from "react-table"
import {Table as BootstrapTable, Input} from "reactstrap"

export default function UserManagerTable({columns,data}){
    const [filter, setFilterBy] = useState({
        userId: '',
        name: '',
        surname: '',
        mail: '',
        phone: '',
        statusofverification: ''
    })

    const filterBy = e => {
    const value = e.target.value || undefined;
    setFilter(e.target.name, value)
    setFilterBy({
        ...filter,
        [e.target.name]: e.target.value,
    });    
    };

    const {
        getTableProps,
        getTableBodyProps,
        headerGroups,
        rows,
        prepareRow,
        setFilter,
      } = useTable(
        {
          columns,
          data,
        },
        useFilters,
        useSortBy
      );

      return (
        <BootstrapTable striped {...getTableProps()}>
        <thead>
          {headerGroups.map((headerGroup) => (
            <tr {...headerGroup.getHeaderGroupProps()}>
              {headerGroup.headers.map((column) => (
                <th {...column.getHeaderProps(column.getSortByToggleProps())}>
                  {column.render("Header")}
                </th>
              ))}
            </tr>
          ))}
          <tr>
          <td>
              <Input
                value={filter.userId}
                onChange={filterBy}
                placeholder={"Search by userId"}
                name="userId"
              />
            </td>
            <td>
              <Input
                value={filter.name}
                onChange={filterBy}
                placeholder={"Search by name"}
                name="name"
              />
            </td>
            <td>
              <Input
                value={filter.surname}
                onChange={filterBy}
                placeholder={"Search by surname"}
                name="surname"
              />
            </td>
            <td>
              <Input
                value={filter.phone}
                onChange={filterBy}
                placeholder={"Search by phone"}
                name="phone"
              />
            </td>
            <td>
              <Input
                value={filter.mail}
                onChange={filterBy}
                placeholder={"Search by mail"}
                name="mail"
              />
            </td>
            <td>
              <Input
                value={filter.mail}
                onChange={filterBy}
                placeholder={"Search by varification"}
                name="statusofverification"
              />
            </td>
          </tr>
        </thead>
        <tbody {...getTableBodyProps()}>
          {rows.map((row, i) => {
            prepareRow(row);
            return (
              <tr {...row.getRowProps()}>
                {row.cells.map((cell) => {
                  return (
                    <td {...cell.getCellProps()}> {cell.render("Cell")} </td>
                  );
                })}
              </tr>
            );
          })}
        </tbody>
      </BootstrapTable>
      )
}