#include <mqueue.h>
#include <stdio.h>
#include <stdlib.h>
#include <errno.h>
#include <time.h>


#include "message.h"

int main(int argc, char** argv) {
	struct mq_attr attr;

	attr.mq_flags = 0;
	attr.mq_maxmsg = 10;
	attr.mq_msgsize = sizeof(message_t);
	attr.mq_curmsgs = 0;

	mqd_t qid = mq_open("/mailbox", O_RDWR | O_CREAT, 
		S_IRUSR | S_IWUSR, &attr);

	if(qid == -1) {
		perror("mq_open");
		return 1;
	}

	srand(time(0));

	int a = 0;
	do {
		message_t msg;

		if(rand() % 100 > 20 || a < 5) {
			msg.command = 0;
			msg.argument = rand() % 10;
			a++;
		} else {
			if(rand() % 100 > 50)
				msg.command = 1;
			else return 1;
		}
		
		if(mq_send(qid, (char *) &msg, sizeof(message_t), 0) == -1) {
			perror("mq_send");
		}

		printf("sender: message sent, cmd == %d, arg == %d\n", 
			msg.command, msg.argument);

		if(msg.command == CMD_KILL)
			break;
		
		sleep(msg.argument - 1);
	} while (1);

	return 0;
}