import { ReactNode } from 'react';
import { Table } from 'react-bootstrap';
import { Pagination } from './Pagination';
import { FilterPanel } from './FilterPanel';
import { SortableTableHeader } from './SortableTableHeader';
import { useTableState } from '../../hooks/useTableState';

interface Column<T> {
  field: keyof T;
  label: string;
  render?: (item: T) => ReactNode;
}

interface DataTableProps<T> {
  data: T[];
  columns: Column<T>[];
  filterConfig: {
    [K in keyof T]: {
      label: string;
      type: 'select' | 'text' | 'number' | 'date';
      options?: { value: any; label: string }[];
    };
  };
  initialSortField: keyof T;
  onRowClick?: (item: T) => void;
}

export const DataTable = <T extends Record<string, any>>({
  data,
  columns,
  filterConfig,
  initialSortField,
  onRowClick,
}: DataTableProps<T>) => {
  const tableState = useTableState<T>(
    {} as T,
    initialSortField
  );

  const {
    pagination: { state: paginationState, controls: paginationControls },
    filter: { state: filterState, controls: filterControls },
    sort: { state: sortState, controls: sortControls },
  } = tableState;

  // Apply filters
  const filteredData = data.filter((item) => {
    const matchesSearch = Object.values(item).some((value) =>
      String(value).toLowerCase().includes(filterState.searchQuery.toLowerCase())
    );

    const matchesFilters = Object.entries(filterState.filters).every(
      ([key, value]) => !value || String(item[key]) === String(value)
    );

    return matchesSearch && matchesFilters;
  });

  // Apply sorting
  const sortedData = [...filteredData].sort((a, b) => {
    const aValue = a[sortState.field];
    const bValue = b[sortState.field];

    if (typeof aValue === 'string' && typeof bValue === 'string') {
      return sortState.direction === 'asc'
        ? aValue.localeCompare(bValue)
        : bValue.localeCompare(aValue);
    }

    return sortState.direction === 'asc'
      ? Number(aValue) - Number(bValue)
      : Number(bValue) - Number(aValue);
  });

  // Apply pagination
  const startIndex = (paginationState.currentPage - 1) * paginationState.itemsPerPage;
  const endIndex = startIndex + paginationState.itemsPerPage;
  const paginatedData = sortedData.slice(startIndex, endIndex);

  // Update total items
  paginationControls.setTotalItems(filteredData.length);

  return (
    <div>
      <FilterPanel
        filters={filterState}
        filterControls={filterControls}
        filterConfig={filterConfig}
      />

      <Table striped hover responsive>
        <thead>
          <tr>
            {columns.map((column) => (
              <SortableTableHeader
                key={String(column.field)}
                field={column.field}
                label={column.label}
                sortState={sortState}
                sortControls={sortControls}
              />
            ))}
          </tr>
        </thead>
        <tbody>
          {paginatedData.map((item, index) => (
            <tr
              key={index}
              onClick={() => onRowClick?.(item)}
              style={{ cursor: onRowClick ? 'pointer' : 'default' }}
            >
              {columns.map((column) => (
                <td key={String(column.field)}>
                  {column.render
                    ? column.render(item)
                    : String(item[column.field])}
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </Table>

      <Pagination
        currentPage={paginationState.currentPage}
        totalPages={Math.ceil(filteredData.length / paginationState.itemsPerPage)}
        itemsPerPage={paginationState.itemsPerPage}
        onPageChange={paginationControls.setCurrentPage}
        onItemsPerPageChange={paginationControls.setItemsPerPage}
      />
    </div>
  );
}; 