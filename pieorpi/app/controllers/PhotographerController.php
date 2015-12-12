<?php

class PhotographerController extends ControllerBase
{
    public function indexAction() {
    	
    }

    public function signupAction() {

    }

    public function loginAction() {

    }

    public function uploadPhotosAction() {

    	if ($this->request->hasFiles() == true) {
                //Print the real file names and their sizes
                foreach ($this->request->getUploadedFiles() as $file){
                        echo $file->getName(), " ", $file->getSize(), "\n";
                        
                        $file_name = time()."_".$file->getName();
                        $file->moveTo(APP_PATH . "/public/files/".$file_name);

                }

                $photoFor = $this->request->getPost('photosFor');
                $this->firebase->push('/photographers/'.$this->session->get('photographer_unique')."/trials/".$photoFor , $file_name );

        }

        $this->response->redirect('photographer/dash'); 

    }

    public function dashAction() {
    	if(!$this->session->has('photographer_unique')) {
    		echo "no!";
    		die();
    	}

    	$photographer = $this->firebase->get('/photographers/'.$this->session->get('photographer_unique'));
    	$this->view->photographer = json_decode($photographer, true);

    	//$this->response->redirect('/photographer/uploadPhotos');

    }

    public function logUserInAction() {
    	$email = $this->request->getPost('email');
    	$password = $this->request->getPost('password');
    	$password = md5($password);

    	if(!$email || $email == '') {
    		echo "problem";
    		die();
    	}

    	$photographer_unique =  str_ireplace('@','_',$email);
    	$photographer_unique =  str_ireplace('.','_',$photographer_unique);

    	$provider = $this->firebase->get('/photographers/'.$photographer_unique);
    	$provider = json_decode($provider, true);

    	if(sizeof($provider) == 0 ) {
    		echo "mno user";
    		die();
    	}

    	if($provider['password'] != $password) {
    		echo "password is wrong";
    		die();
    	}


    	$this->session->set('online', '1');
    	$this->session->set('photographer_unique', $photographer_unique);

    }

    public function createPhotographerAction() {
    	$email = $this->request->getPost('email');
    	$password = $this->request->getPost('password');


    	if(!$email || $email == '') {
    		echo "problem";
    		die();
    	}

    	$photographer_unique =  str_ireplace('@','_',$email);
    	$photographer_unique =  str_ireplace('.','_',$photographer_unique);


    	$photographer['email'] = $email;
    	$photographer['password'] = md5($password);
    	$photographer['experience'] = 0;


    	$this->firebase->set('/photographers/'.$photographer_unique, $photographer );
    	$this->response->redirect('photographer/login'); 
    }




    function crypto_rand_secure($min, $max) {
	        $range = $max - $min;
	        if ($range < 0) return $min; // not so random...
	        $log = log($range, 2);
	        $bytes = (int) ($log / 8) + 1; // length in bytes
	        $bits = (int) $log + 1; // length in bits
	        $filter = (int) (1 << $bits) - 1; // set all lower bits to 1
	        do {
	            $rnd = hexdec(bin2hex(openssl_random_pseudo_bytes($bytes)));
	            $rnd = $rnd & $filter; // discard irrelevant bits
	        } while ($rnd >= $range);
	        return $min + $rnd;
	}

	function getToken($length=32){
	    $token = "";
	    $codeAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	    $codeAlphabet.= "abcdefghijklmnopqrstuvwxyz";
	    $codeAlphabet.= "0123456789";
	    for($i=0;$i<$length;$i++){
	        $token .= $codeAlphabet[$this->crypto_rand_secure(0,strlen($codeAlphabet))];
	    }
	    return $token;
	}

	function logoutAction() {
		$this->session->destroy();
		$this->response->redirect('/');
	}
}

