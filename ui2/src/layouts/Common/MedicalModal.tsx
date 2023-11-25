import CloseIcon from '@mui/icons-material/Close';
import Dialog from '@mui/material/Dialog';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import IconButton from '@mui/material/IconButton';
import { styled } from '@mui/material/styles';
import * as React from 'react';

export interface MyModalProps {

  open: boolean;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
  children?: React.ReactNode;
  title?: string;
  fullScreen?: boolean;
  // onSave: () => void;
}
export interface DialogTitleProps {
  id: string;
  children?: React.ReactNode;
  onClose: () => void;

}


const BootstrapDialog = styled(Dialog)(({ theme }) => ({
  '& .MuiDialogContent-root': {
    padding: theme.spacing(2),
  },
  '& .MuiDialogActions-root': {
    padding: theme.spacing(1),
  },
}));




const MedicalModal: React.FC<MyModalProps> = ({ open, setOpen, children, title, fullScreen }) => {

  const handleClickOpen = () => {
    setOpen(true);
  };
  const handleClose = (event: any, reason: any) => {
    if (reason !== 'backdropClick') {
      setOpen(false)
    }

  };

  return (
    <div>
      <BootstrapDialog
        fullWidth={true}
        onClose={handleClose}
        aria-labelledby="customized-dialog-title"
        open={open}
        fullScreen={fullScreen ?? true}
      >
        <BootstrapDialogTitle id="customized-dialog-title" onClose={handleClose as any}>
          {title ?? "Modal title"}
        </BootstrapDialogTitle>
        <DialogContent dividers>
          {children}
        </DialogContent>
        {/* <DialogActions>
          <Button autoFocus variant="contained" onClick={() => {
            onSave();
            setOpen(false);
          }}>
            Save changes
          </Button>
        </DialogActions> */}
      </BootstrapDialog>
    </div>
  );
}


export default MedicalModal




function BootstrapDialogTitle(props: DialogTitleProps) {
  const { children, onClose, ...other } = props
  return (
    <DialogTitle sx={{ m: 0, p: 2 }} {...other}>
      {children}
      {onClose ? (
        <IconButton
          aria-label="close"
          onClick={onClose}
          sx={{
            position: 'absolute',
            right: 8,
            top: 8,
            color: (theme) => theme.palette.grey[500],
          }}
        >
          <CloseIcon />
        </IconButton>
      ) : null}
    </DialogTitle>
  );
}